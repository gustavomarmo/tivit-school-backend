using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("topico")]
    public class Topico
    {
        public int id { get; set; }
        public string titulo { get; set; }

        public int turmaDisciplinaId { get; set; }
        public TurmaDisciplina turmaDisciplina { get; set; } = null!;

        public int? turmaExtracurricularId { get; set; }
        public TurmaExtracurricular? turmaExtracurricular { get; set; }

        public List<Material> materiais { get; set; } = new();
    }
}