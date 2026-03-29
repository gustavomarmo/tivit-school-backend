using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO
{
    public class ValidarOtpDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Codigo { get; set; } = string.Empty;
    }
}