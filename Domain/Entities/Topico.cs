using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Domain.Entities
{
    [Table("topico")]
    public class Topico
    {
        public int id { get; set; }
        public string titulo { get; set; }

        public int turmaDisciplinaId { get; set; }
        public TurmaDisciplina turmaDisciplina { get; set; } = null!;

        public List<Material> materiais { get; set; } = new();
    }
}