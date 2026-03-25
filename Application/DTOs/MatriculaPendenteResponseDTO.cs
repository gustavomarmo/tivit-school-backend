namespace edu_connect_backend.Application.DTOs
{
    public class MatriculaPendenteResponseDTO
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string cpf { get; set; } = string.Empty;
        public string telefone { get; set; } = string.Empty;
        public string? nomeResponsavel { get; set; }
        public string? serie { get; set; }
        public string? turno { get; set; }
        public decimal? mensalidade { get; set; }
        public string status { get; set; } = string.Empty;
        public DateTime dataSolicitacao { get; set; }
        public List<DocumentoMatriculaResponseDTO> documentos { get; set; } = new();
    }
}
