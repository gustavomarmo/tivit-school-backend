using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Auth
{
    public class ResetarSenhaRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string NovaSenha { get; set; } = string.Empty;
    }
}