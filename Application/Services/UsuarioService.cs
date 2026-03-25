using edu_connect_backend.Domain.Entities;
using edu_connect_backend.Infrastructure.Email;
using edu_connect_backend.Infrastructure.Persistence.Repositories;

namespace edu_connect_backend.Application.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository usuarioRepository;
        private readonly EmailService emailService;

        public UsuarioService(UsuarioRepository usuarioRepository, EmailService emailService)
        {
            this.usuarioRepository = usuarioRepository;
            this.emailService = emailService;
        }

        public Usuario? Autenticar(string email, string senha)
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmail(email)
                ?? throw new KeyNotFoundException("Usuário não encontrado");

            if (!BCrypt.Net.BCrypt.Verify(senha, usuario.senhaHash))
                return null;

            return usuario;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            if (usuarioRepository.ObterUsuarioPorEmail(usuario.email) != null)
                throw new InvalidOperationException("E-mail já cadastrado.");

            usuarioRepository.AdicionarUsuario(usuario);
        }

        public async Task SolicitarResetSenhaAsync(string email)
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmail(email);

            if (usuario == null) return;

            string otp = new Random().Next(100000, 999999).ToString();
            usuario.codigoOtp = otp;
            usuario.validadeOtp = DateTime.Now.AddMinutes(15);

            usuarioRepository.AtualizarUsuario(usuario);

            string assunto = "Recuperação de Senha - TIVIT School";
            string corpoHtml = $@"
                <h3>Olá, {usuario.nome}!</h3>
                <p>Você solicitou a redefinição de sua senha.</p>
                <p>Seu código de verificação é: <strong>{otp}</strong></p>
                <p>Este código é válido por 15 minutos. Se você não solicitou esta alteração, ignore este e-mail.</p>";

            await emailService.SendEmailAsync(usuario.email, usuario.nome, assunto, "", corpoHtml);
        }

        public bool ValidarOtpSenha(string email, string codigo)
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmail(email);

            if (usuario == null || usuario.codigoOtp != codigo || usuario.validadeOtp < DateTime.Now)
                return false;

            return true;
        }

        public bool ResetarSenha(string email, string codigo, string novaSenha)
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmail(email);

            if (usuario == null || usuario.codigoOtp != codigo || usuario.validadeOtp < DateTime.Now)
                return false;

            usuario.senhaHash = BCrypt.Net.BCrypt.HashPassword(novaSenha);
            usuario.codigoOtp = null;
            usuario.validadeOtp = null;

            usuarioRepository.AtualizarUsuario(usuario);
            return true;
        }
    }
}