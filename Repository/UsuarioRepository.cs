using edu_connect_backend.Context;
using edu_connect_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class UsuarioRepository
    {
        private readonly ConnectionContext context;

        public UsuarioRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public Usuario? ObterUsuarioPorEmail(string email)
        {
            return context.usuarios.FirstOrDefault(u => u.email == email);
        }

        public Usuario? ObterPorId(int usuarioId)
        {
            return context.usuarios.FirstOrDefault(u => u.id == usuarioId);
        }

        public Usuario? obterPorUsuarioId(int usuarioId)
        {
            return ObterPorId(usuarioId);
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            context.usuarios.Add(usuario);
            context.SaveChanges();
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            context.usuarios.Update(usuario);
            context.SaveChanges();
        }
    }
}