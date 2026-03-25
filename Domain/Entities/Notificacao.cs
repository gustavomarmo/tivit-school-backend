using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Domain.Entities
{
    [Table("notificacao")]
    public class Notificacao
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string mensagem { get; set; }
        public string tipo { get; set; }
        public DateTime dataCriacao { get; set; } = DateTime.Now;
        public bool lida { get; set; } = false;
        public int usuarioId { get; set; }
        public Usuario usuario { get; set; }
    }
}