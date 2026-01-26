namespace edu_connect_backend.Model
{
    public class DashboardAlunoReadModel
    {
        public List<NotaResumoReadModel> ultimasNotas { get; set; } = new();
        public List<AvisoResumoReadModel> avisos { get; set; } = new();
        public List<TarefaPendenteReadModel> tarefasPendentes { get; set; } = new();
    }
}