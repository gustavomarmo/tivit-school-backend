using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Domain.Entities
{
    [Table("turma_disciplina")]
    public class TurmaDisciplina
    {
        public int id { get; set; }

        public int turmaId { get; set; }
        public Turma turma { get; set; } = null!;

        public int disciplinaId { get; set; }
        public Disciplina disciplina { get; set; } = null!;

        public int professorId { get; set; }
        public Professor professor { get; set; } = null!;

        public List<Topico> topicos { get; set; } = new();
    }
}