using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("material")]
    public class Material
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string tipo { get; set; }
        public string? url { get; set; }

        public int topicoId { get; set; }
        public Topico topico { get; set; } = null!;

        public DateTime? dataEntrega { get; set; }
        public decimal? notaMaxima { get; set; }
    }
}