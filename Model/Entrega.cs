using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("entrega")]
    public class Entrega
    {
        public int id { get; set; }
        public DateTime dataEntrega { get; set; } = DateTime.Now;
        public string arquivoUrl { get; set; } = string.Empty;

        public int materialId { get; set; }
        public Material material { get; set; }

        public int alunoId { get; set; }
        public Aluno aluno { get; set; }
    }
}