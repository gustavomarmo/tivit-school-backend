using edu_connect_backend.Model;

namespace edu_connect_backend.DTO
{
    public class DashboardAlunoDTO
    {
        public List<NotaResumoReadModel> ultimasNotas { get; set; } = new();
        public List<AvisoResumoReadModel> avisos { get; set; } = new();
        public List<TarefaPendenteReadModel> tarefasPendentes { get; set; } = new();
    }
}