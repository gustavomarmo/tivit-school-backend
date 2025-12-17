using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("aluno")]
    public class Aluno
    {
        public int id { get; set; }

        public string matricula { get; set; } = string.Empty;

        public DateTime dataNascimento { get; set; }

        // Chaves Estrangeiras
        public int usuario_id { get; set; }
        public Usuario usuario { get; set; } = null!;

        public int? turma_id { get; set; }
        public Turma? turma { get; set; }
    }
}
