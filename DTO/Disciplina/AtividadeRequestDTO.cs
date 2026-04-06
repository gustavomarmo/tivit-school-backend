using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Disciplina
{
    public class AtividadeRequestDTO
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [MaxLength(150, ErrorMessage = "O título deve ter no máximo 150 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data de entrega é obrigatória")]
        public DateTime DataEntrega { get; set; }

        [Required(ErrorMessage = "A nota máxima é obrigatória")]
        [Range(0, 10, ErrorMessage = "A nota máxima deve ser entre 0 e 10")]
        public decimal NotaMaxima { get; set; }

        [Required(ErrorMessage = "O ID do tópico é obrigatório")]
        public int TopicoId { get; set; }
    }
}