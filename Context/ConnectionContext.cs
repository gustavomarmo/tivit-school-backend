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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Configurar Enum para salvar como Texto (Opcional, mas recomendado para leitura)
            modelBuilder.Entity<Usuario>()
                .Property(u => u.perfil)
                .HasConversion<string>();

            // 2. LÓGICA PARA CONVERTER TUDO PARA SNAKE_CASE
            // Percorre todas as tabelas e colunas definidas
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Converte nome da tabela (Ex: TurmaDisciplinas -> turma_disciplinas)
                if (entity.GetTableName() != null)
                {
                    entity.SetTableName(ToSnakeCase(entity.GetTableName()!));
                }

                // Converte nome das colunas (Ex: DataNascimento -> data_nascimento)
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.Name));
                }
            }
        }

        // Função auxiliar para converter String (PascalCase -> snake_case)
        private string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

    }
}
