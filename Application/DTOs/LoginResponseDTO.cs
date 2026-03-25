namespace edu_connect_backend.Application.DTOs
{
    public class LoginResponseDTO
    {
        public string email { get; set; }
        public string nome { get; set; }
        public string token { get; set; }
        public string perfil { get; set; }
        public string? fotoUrl { get; set; }
    }
}
