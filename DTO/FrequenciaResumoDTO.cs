namespace edu_connect_backend.DTO
{
    public class FrequenciaResumoDTO
    {
        public string Disciplina { get; set; } = string.Empty;
        public int TotalAulas { get; set; }
        public int Faltas { get; set; }
        public decimal Frequencia { get; set; }
    }
}