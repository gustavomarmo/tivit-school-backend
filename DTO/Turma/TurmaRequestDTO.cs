using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Turma
{
    public class TurmaRequestDTO
    {
        [Required(ErrorMessage = "O nome da turma é obrigatório")]
        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ano letivo é obrigatório")]
        [Range(2000, 2100, ErrorMessage = "Ano letivo inválido")]
        public int AnoLetivo { get; set; }
    }
}
