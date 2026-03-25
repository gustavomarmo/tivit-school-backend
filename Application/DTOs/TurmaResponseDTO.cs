namespace edu_connect_backend.Application.DTOs
{
    public class TurmaResponseDTO
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public int anoLetivo { get; set; }
    }
}