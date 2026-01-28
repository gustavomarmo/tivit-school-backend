namespace edu_connect_backend.Model
{
    public class NotaLancamentoReadModel
    {
        public string Matricula { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public double? N1 { get; set; }
        public double? N2 { get; set; }
        public double? Ativ { get; set; }
    }
}