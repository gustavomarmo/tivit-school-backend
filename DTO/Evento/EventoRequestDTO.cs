using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Evento
{
    public class EventoRequestDTO
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [MaxLength(150, ErrorMessage = "O título deve ter no máximo 150 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória")]
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório")]
        [MaxLength(50, ErrorMessage = "O tipo deve ter no máximo 50 caracteres")]
        public string Tipo { get; set; }
        public int? TurmaId { get; set; }
    }
}
