using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Auth
{
    public class EsqueciSenhaRequestDTO
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; } = string.Empty;
    }
}