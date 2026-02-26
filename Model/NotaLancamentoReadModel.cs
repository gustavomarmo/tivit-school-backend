namespace edu_connect_backend.Model
{
    public class NotaLancamentoReadModel
    {
        public int AlunoId { get; set; }
        public string Matricula { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public double? N1_B1 { get; set; }
        public double? N2_B1 { get; set; }
        public double? Ativ_B1 { get; set; }
        public double? N1_B2 { get; set; }
        public double? N2_B2 { get; set; }
        public double? Ativ_B2 { get; set; }
    }
}