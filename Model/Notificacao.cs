using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("notificacao")]
    public class Notificacao
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string mensagem { get; set; }
        public string tipo { get; set; } // 'success', 'warning', 'info', etc.
        public DateTime dataCriacao { get; set; } = DateTime.Now;
        public bool lida { get; set; } = false;

        // Relacionamento com Usuário (para saber de quem é a notificação)
        public int usuarioId { get; set; }
        public Usuario usuario { get; set; }
    }
}