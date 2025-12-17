using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Context
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }
        public DbSet<Model.Usuario> usuarios { get; set; }

    }
}
