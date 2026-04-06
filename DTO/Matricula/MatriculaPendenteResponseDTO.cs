namespace edu_connect_backend.DTO.Matricula
{
    public class MatriculaPendenteResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string? NomeResponsavel { get; set; }
        public string? Serie { get; set; }
        public string? Turno { get; set; }
        public decimal? Mensalidade { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime DataSolicitacao { get; set; }
        public List<DocumentoMatriculaResponseDTO> Documentos { get; set; } = new();
    }
}
