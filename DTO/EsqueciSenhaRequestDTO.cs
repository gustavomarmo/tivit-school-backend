using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO
{
    public class EsqueciSenhaRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}