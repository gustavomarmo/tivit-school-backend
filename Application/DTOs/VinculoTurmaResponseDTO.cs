namespace edu_connect_backend.Application.DTOs
{
    public class VinculoTurmaResponseDTO
    {
        public int id { get; set; }
        public int disciplinaId { get; set; }
        public string disciplina { get; set; } = string.Empty;
        public int professorId { get; set; }
        public string professor { get; set; } = string.Empty;
    }
}
