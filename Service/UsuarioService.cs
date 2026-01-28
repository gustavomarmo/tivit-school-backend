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
            var usuario = usuarioRepository.ObterUsuarioPorEmail(email);

            if (usuario == null)
                return null;

            if (usuario.senhaHash != senha)
                return null;

            return usuario;
        }

        public Usuario? ObterUsuarioPorEmail(string email)
        {
            return usuarioRepository.ObterUsuarioPorEmail(email);
        }

        public Usuario? ObterPorId(int id)
        {
            return usuarioRepository.ObterPorId(id);
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            // Validações básicas de negócio podem vir aqui
            if (usuarioRepository.ObterUsuarioPorEmail(usuario.email) != null)
                throw new Exception("E-mail já cadastrado.");

            usuarioRepository.AdicionarUsuario(usuario);
        }
    }
}