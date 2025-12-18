using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("disciplina")]
    public class Disciplina
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string codigo { get; set; }
    }
}