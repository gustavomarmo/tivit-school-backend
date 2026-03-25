namespace edu_connect_backend.Application.DTOs
{
    public class NotaLancamentoDTO
    {
        public int AlunoId { get; set; }
        public string Matricula { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public decimal? N1b1 { get; set; }
        public decimal? N2b1 { get; set; }
        public decimal? Ativb1 { get; set; }
        public decimal? N1b2 { get; set; }
        public decimal? N2b2 { get; set; }
        public decimal? Ativb2 { get; set; }
    }
}