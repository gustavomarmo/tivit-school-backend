namespace edu_connect_backend.DTO.Matricula
{
    public class DocumentoMatriculaResponseDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string NomeOriginal { get; set; } = string.Empty;
        public bool Validado { get; set; }
    }
}
