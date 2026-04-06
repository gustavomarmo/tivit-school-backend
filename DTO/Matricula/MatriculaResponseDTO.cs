namespace edu_connect_backend.DTO.Matricula
{
    public class MatriculaResponseDTO
    {
        public int IdSolicitacao { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string? Endereco { get; set; }
        public string? NomeResponsavel { get; set; }
        public string? ContatoResponsavel { get; set; }
        public string? Escolaridade { get; set; }
        public string? Serie { get; set; }
        public string? Turno { get; set; }
        public decimal? Mensalidade { get; set; }
    }
}
