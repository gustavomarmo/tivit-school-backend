namespace edu_connect_backend.DTO
{
    public class MatriculaResponseDTO
    {
        public int idSolicitacao { get; set; }
        public string status { get; set; } = string.Empty;
        public string nome { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string cpf { get; set; } = string.Empty;
        public string telefone { get; set; } = string.Empty;
        public string? endereco { get; set; }
        public string? nomeResponsavel { get; set; }
        public string? contatoResponsavel { get; set; }
        public string? escolaridade { get; set; }
        public string? serie { get; set; }
        public string? turno { get; set; }
        public decimal? mensalidade { get; set; }
    }
}
