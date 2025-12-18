using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("nota")]
    public class Nota
    {
        public int id { get; set; }
        public decimal valor { get; set; }
        public string descricao { get; set; }
        public DateTime dataLancamento { get; set; }

        public int alunoId { get; set; }
        public Aluno aluno { get; set; } = null!;

        public int turmaDisciplinaId { get; set; }
        public TurmaDisciplina turmaDisciplina { get; set; } = null!;
    }
}