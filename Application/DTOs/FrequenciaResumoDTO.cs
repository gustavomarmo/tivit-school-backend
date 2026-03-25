namespace edu_connect_backend.Application.DTOs
{
    public class FrequenciaResumoDTO
    {
        public string disciplina { get; set; } = string.Empty;
        public int totalAulas { get; set; }
        public int faltas { get; set; }
        public double frequencia { get; set; }
    }
}