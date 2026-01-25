using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    public class AvisoResumoReadModel
    {
        public string titulo { get; set; }
        public string mensagem { get; set; }
        public DateTime data { get; set; }
        public string autor { get; set; }
    }
}
