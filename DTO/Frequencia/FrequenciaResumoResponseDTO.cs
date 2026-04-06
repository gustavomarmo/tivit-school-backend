namespace edu_connect_backend.DTO.Frequencia
{
    public class FrequenciaResumoResponseDTO
    {
        public string Disciplina { get; set; } = string.Empty;
        public int TotalAulas { get; set; }
        public int Faltas { get; set; }
        public double Frequencia { get; set; }
    }
}