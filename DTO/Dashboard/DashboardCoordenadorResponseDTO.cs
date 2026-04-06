using edu_connect_backend.DTO.Evento;

namespace edu_connect_backend.DTO.Dashboard
{
    public class DashboardCoordenadorResponseDTO
    {
        public KPIsCoordenadorDTO Kpis { get; set; }
        public List<GraficoBarrasDTO> GraficoDesempenho { get; set; } = new();
        public List<GraficoPizzaDTO> GraficoStatus { get; set; } = new();
        public List<EventoResponseDTO> ProximosEventos { get; set; } = new();
    }

    public class KPIsCoordenadorDTO
    {
        public int TotalAlunos { get; set; }
        public int TotalProfessores { get; set; }

        public int TotalTurmas { get; set; }
        public decimal MediaGeralEscola { get; set; }
    }

    public class GraficoBarrasDTO
    {
        public string Label { get; set; }
        public decimal Value { get; set; }
    }

    public class GraficoPizzaDTO
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }
}