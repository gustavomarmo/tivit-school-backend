namespace edu_connect_backend.Application.DTOs
{
    public class DocumentoMatriculaResponseDTO
    {
        public int id { get; set; }
        public string tipo { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
        public string nomeOriginal { get; set; } = string.Empty;
        public bool validado { get; set; }
    }
}
