using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("extracurricular")]
    public class Extracurricular
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string? descricao { get; set; }
    }
