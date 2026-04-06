using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Auth
{
    public class ValidarOtpRequestDTO
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O código OTP é obrigatório")]
        public string Codigo { get; set; } = string.Empty;
    }
}