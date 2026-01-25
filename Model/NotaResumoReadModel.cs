using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    public class NotaResumoReadModel
    {
        public string materia { get; set; }
        public string descricao { get; set; }
        public decimal valor { get; set; }
        public DateTime data { get; set; }
    }
}
