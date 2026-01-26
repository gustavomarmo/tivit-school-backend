using edu_connect_backend.Context;
using edu_connect_backend.Model;

namespace edu_connect_backend.Repository
{
    public class UsuarioRepository
    {
        private readonly ConnectionContext context;
        
        public UsuarioRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public Usuario? obterUsuarioPorEmail(string email)
        {
            return context.usuarios.FirstOrDefault(u => u.email == email);
        }

        public Usuario? obterPorUsuarioId(int usuarioId)
        {
            return context.usuarios.FirstOrDefault(u => u.id == usuarioId);
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            context.usuarios.Add(usuario);
            context.SaveChanges();
        }

    }
}
