using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Matricula
{
    public class MatriculaInicialDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; } = string.Empty;

        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data de nascimento obrigatória")]
        public DateTime DataNascimento { get; set; }
    }
}