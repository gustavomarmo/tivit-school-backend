using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Matricula
{
    public class MatriculaInicialRequestDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [MaxLength(14, ErrorMessage = "CPF inválido")]
        [MinLength(11, ErrorMessage = "CPF inválido")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [MaxLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [MaxLength(20, ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }
    }
}