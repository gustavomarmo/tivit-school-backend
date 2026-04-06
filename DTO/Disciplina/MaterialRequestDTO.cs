using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Disciplina
{
    public class MaterialRequestDTO

    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [MaxLength(150, ErrorMessage = "O título deve ter no máximo 150 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório")]
        [MaxLength(20, ErrorMessage = "O tipo deve ter no máximo 20 caracteres")]
        public string Tipo { get; set; }

        [MaxLength(500, ErrorMessage = "A URL deve ter no máximo 500 caracteres")]
        public string? Url { get; set; }

        [Required(ErrorMessage = "O ID do tópico é obrigatório")]
        public int TopicoId { get; set; }
    }
}