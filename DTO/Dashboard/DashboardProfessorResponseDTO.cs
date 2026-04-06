namespace edu_connect_backend.DTO.Dashboard
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
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Turma { get; set; }
        public string Disciplina { get; set; }
        public decimal Media { get; set; }
        public string? Foto { get; set; }
    }
}