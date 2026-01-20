using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("turma_extracurricular")]
    public class TurmaExtracurricular
    {
        public int id { get; set; }

        public int turmaId { get; set; }
        public Turma turma { get; set; } = null!;

        public int extracurricularId { get; set; }
        public Extracurricular extracurricular { get; set; } = null!;

        public int professorId { get; set; }
        public Professor professor { get; set; } = null!;
        public List<Topico> topicos { get; set; } = new();
    }
}