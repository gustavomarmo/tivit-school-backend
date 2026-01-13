using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("frequencia")]
    public class Frequencia
    {
        public int id { get; set; }
        public DateTime dataAula { get; set; } = DateTime.Now;
        public bool presente { get; set; }

        public int alunoId { get; set; }
        public Aluno aluno { get; set; }

        public int disciplinaId { get; set; }
        public Disciplina disciplina { get; set; }
    }
}