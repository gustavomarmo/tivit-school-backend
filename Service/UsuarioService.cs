using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class UsuarioService
    {
        private readonly UsuarioRepository usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public Usuario? Autenticar(string email, string senha)
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmail(email)
                ?? throw new KeyNotFoundException("Usuário não encontrado");

            if (usuario.senhaHash != senha)
                return null;

            return usuario;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            if (usuarioRepository.ObterUsuarioPorEmail(usuario.email) != null)
                throw new InvalidOperationException("E-mail já cadastrado.");

            usuarioRepository.AdicionarUsuario(usuario);
        }
    }
}