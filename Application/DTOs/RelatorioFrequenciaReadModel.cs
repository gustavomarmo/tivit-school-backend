namespace edu_connect_backend.Application.DTOs
{
    public class RelatorioFrequenciaReadModel
    {
        public int alunoId { get; set; }
        public string nomeAluno { get; set; } = string.Empty;
        public int totalPresencas { get; set; }
        public int totalFaltas { get; set; }
        public double porcentagemFrequencia { get; set; }
    }
}