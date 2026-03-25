namespace edu_connect_backend.Application.DTOs
{
    public class FrequenciaResumoReadModel
    {
        public string disciplina { get; set; } = string.Empty;
        public int totalAulas { get; set; }
        public int totalFaltas { get; set; }
        public double frequencia { get; set; }
    }
}