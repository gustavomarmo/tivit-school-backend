using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Disciplina
{
    public class TopicoRequestDTO
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [MaxLength(150, ErrorMessage = "O título deve ter no máximo 150 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O ID da turma disciplina é obrigatório")]
        public int TurmaDisciplinaId { get; set; }
    }
}
