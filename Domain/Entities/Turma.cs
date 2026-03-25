using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Domain.Entities
{
    [Table("turma")]
    public class Turma
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public int anoLetivo { get; set; }
        
    }
}
