using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Domain.Entities
{
    [Table("professor")]
    public class Professor
    {
        public int id { get; set; }

        public string matricula { get; set; }

        public string especialidade { get; set; }

        public int usuarioId { get; set; }
        public Usuario usuario { get; set; } = null!;
    }
}