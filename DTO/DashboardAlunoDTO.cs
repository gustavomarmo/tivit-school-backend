namespace edu_connect_backend.DTO
{
    // O Objeto principal que o Frontend vai receber
    public class DashboardAlunoDTO
    {
        public List<NotaResumoDTO> ultimasNotas { get; set; } = new();
        public List<AvisoResumoDTO> avisos { get; set; } = new();
        public List<TarefaPendenteDTO> tarefasPendentes { get; set; } = new();
    }

    // Itens individuais (Mapeados do SQL)
    public class NotaResumoDTO
    {
        public string materia { get; set; }
        public string descricao { get; set; }
        public decimal valor { get; set; }
        public DateTime data { get; set; }
    }

    public class AvisoResumoDTO
    {
        public string titulo { get; set; }
        public string mensagem { get; set; }
        public DateTime data { get; set; }
        public string autor { get; set; }
    }

    public class TarefaPendenteDTO
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string materia { get; set; }
        public DateTime? prazo { get; set; }
    }
}