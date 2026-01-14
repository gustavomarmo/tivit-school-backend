using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace edu_connect_backend.Context
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }
        public DbSet<Model.Usuario> usuarios { get; set; }
        public DbSet<Aluno> alunos { get; set; }
        public DbSet<Turma> turmas { get; set; }
        public DbSet<Professor> professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<TurmaDisciplina> TurmaDisciplinas { get; set; }
        public DbSet<Topico> Topicos { get; set; }
        public DbSet<Material> Materiais { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Aviso> Avisos { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<Entrega> Entregas { get; set; }

        public DbSet<Frequencia> Frequencias { get; set; }
        public DbSet<Evento> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasKey(u => u.id);
            modelBuilder.Entity<Aluno>().HasKey(a => a.id);
            modelBuilder.Entity<Turma>().HasKey(t => t.id);
            modelBuilder.Entity<Professor>().HasKey(p => p.id);
            modelBuilder.Entity<KPIsCoordenadorDTO>().HasNoKey();
            modelBuilder.Entity<GraficoBarrasDTO>().HasNoKey();
            modelBuilder.Entity<GraficoPizzaDTO>().HasNoKey();

            modelBuilder.Entity<Nota>()
            .HasOne(n => n.aluno)
            .WithMany()
            .HasForeignKey(n => n.alunoId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Notificacao>()
            .HasOne(n => n.usuario)
            .WithMany()
            .HasForeignKey(n => n.usuarioId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Entrega>()
            .HasOne(e => e.material)
            .WithMany()
            .HasForeignKey(e => e.materialId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Entrega>()
            .HasOne(e => e.aluno)
            .WithMany()
            .HasForeignKey(e => e.alunoId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.perfil)
                .HasConversion<string>();

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.GetTableName() != null)
                {
                    entity.SetTableName(ToSnakeCase(entity.GetTableName()!));
                }

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.Name));
                }
            }
        }

        private string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

    }
}
