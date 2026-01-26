using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class UsuarioService
    {
        private readonly UsuarioRepository repository;

        public UsuarioService(UsuarioRepository repository)
        {
            this.repository = repository;
        }

        public Usuario? obterUsuarioPorEmail(string email)
        {
            return repository.obterUsuarioPorEmail(email);
        }

        public void cadastrarUsuario(Usuario usuario)
        {
            repository.AdicionarUsuario(usuario);
        }

    }
}
