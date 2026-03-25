namespace edu_connect_backend.Application.DTOs
{
    public class DashboardProfessorResponseDTO
    {
        public KPIsProfessorDTO kpis { get; set; }
        public List<AlunoAtencaoDTO> alunosAtencao { get; set; } = new();
    }

    public class KPIsProfessorDTO
    {
        public decimal mediaTurmas { get; set; }
        public decimal frequencia { get; set; }
        public int alunosEmRec { get; set; }
    }

    public class AlunoAtencaoDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string turma { get; set; }
        public string disciplina { get; set; }
        public decimal media { get; set; }
        public string? foto { get; set; }
    }
}