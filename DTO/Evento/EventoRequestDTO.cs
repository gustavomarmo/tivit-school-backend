namespace edu_connect_backend.DTO.Evento
{
    public class EventoRequestDTO
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Tipo { get; set; }
        public int? TurmaId { get; set; }
    }
}
