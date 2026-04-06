using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Professor
{
    public class ProfessorRequestDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória")]
        [MaxLength(20, ErrorMessage = "A matrícula deve ter no máximo 20 caracteres")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [MaxLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A especialidade é obrigatória")]
        [MaxLength(100, ErrorMessage = "A especialidade deve ter no máximo 100 caracteres")]
        public string Especialidade { get; set; }
        public bool Ativo { get; set; } = true;
    }
}