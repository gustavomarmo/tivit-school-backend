namespace edu_connect_backend.DTO.Auth
{
    public class LoginResponseDTO
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }
        public string Perfil { get; set; }
        public string? FotoUrl { get; set; }
    }
}
