namespace edu_connect_backend.Model
{
    public class NotaLancamentoReadModel
    {
        public int alunoId { get; set; }
        public string matricula { get; set; } = string.Empty;
        public string nome { get; set; } = string.Empty;
        public decimal? n1B1 { get; set; }
        public decimal? n2B1 { get; set; }
        public decimal? ativB1 { get; set; }
        public decimal? n1B2 { get; set; }
        public decimal? n2B2 { get; set; }
        public decimal? ativB2 { get; set; }
    }
}