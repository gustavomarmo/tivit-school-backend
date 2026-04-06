namespace edu_connect_backend.DTO.Evento
{
    public class EventoResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Inicio { get; set; }
        public string? Fim { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string? TurmaNome { get; set; }
    }
}