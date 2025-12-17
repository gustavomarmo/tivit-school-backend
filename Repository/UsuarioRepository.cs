using edu_connect_backend.Context;
using edu_connect_backend.Model;

namespace edu_connect_backend.Repository
{
    public class UsuarioRepository
    {
        private readonly ConnectionContext context;
        
        public UsuarioRepository(ConnectionContext context)
        {
            context = context;
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

    }
}
