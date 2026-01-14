using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("evento")]
    public class Evento
    {
        public int id { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string descricao { get; set; } = string.Empty;
        public DateTime dataInicio { get; set; }
        public DateTime? dataFim { get; set; }
        public string tipo { get; set; } = "evento";
        public int? turmaId { get; set; }
        public Turma? turma { get; set; }
        public int usuarioCriadorId { get; set; }
        public Usuario usuarioCriador { get; set; }
    }
}