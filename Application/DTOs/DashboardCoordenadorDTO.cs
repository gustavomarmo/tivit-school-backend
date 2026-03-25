namespace edu_connect_backend.Application.DTOs
{
    public class DashboardCoordenadorResponseDTO
    {
        public KPIsCoordenadorDTO kpis { get; set; }
        public List<GraficoBarrasDTO> graficoDesempenho { get; set; } = new();
        public List<GraficoPizzaDTO> graficoStatus { get; set; } = new();
        public List<EventoResponseDTO> proximosEventos { get; set; } = new();
    }

    public class KPIsCoordenadorDTO
    {
        public int totalAlunos { get; set; }
        public int totalProfessores { get; set; }
        public int totalTurmas { get; set; }
        public decimal mediaGeralEscola { get; set; }
    }

    public class GraficoBarrasDTO
    {
        public string label { get; set; }
        public decimal value { get; set; }
    }

    public class GraficoPizzaDTO
    {
        public string label { get; set; }
        public int value { get; set; }
    }
}