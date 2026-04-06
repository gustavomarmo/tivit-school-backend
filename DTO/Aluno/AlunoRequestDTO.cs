using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Aluno
{
    public class AlunoRequestDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória")]
        [MaxLength(20)]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        public int? TurmaId { get; set; }
        public bool Ativo { get; set; } = true;

    }
}
