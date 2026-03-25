using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Domain.Entities
{
    [Table("nota")]
    public class Nota
    {
        public int id { get; set; }
        public decimal valor { get; set; }
        public int bimestre { get; set; }
        public string tipo { get; set; } = string.Empty;
        public DateTime dataLancamento { get; set; } = DateTime.Now;
        public string descricao { get; set; } = string.Empty;

        public int alunoId { get; set; }
        public Aluno? aluno { get; set; }

        public int turmaDisciplinaId { get; set; }
        public TurmaDisciplina? turmaDisciplina { get; set; }

        [NotMapped]
        public int TempTurmaId { get; set; }
        [NotMapped]
        public int TempDisciplinaId { get; set; }
    }
}