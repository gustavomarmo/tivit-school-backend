namespace edu_connect_backend.DTO
{
    public class EventoRequestDTO
    {
        public string titulo { get; set; }
        public string descricao { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime? dataFim { get; set; }
        public string tipo { get; set; }
        public int? turmaId { get; set; }
    }

    public class EventoResponseDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string? end { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string? turmaNome { get; set; }
    }
}