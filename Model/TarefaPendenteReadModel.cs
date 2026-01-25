namespace edu_connect_backend.Model
{
    public class TarefaPendenteReadModel
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string materia { get; set; }
        public DateTime? prazo { get; set; }
    }
}
