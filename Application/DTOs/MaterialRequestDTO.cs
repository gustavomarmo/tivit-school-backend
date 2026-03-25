namespace edu_connect_backend.Application.DTOs
{
    public class TopicoRequestDTO
    {
        public string titulo { get; set; }
        public int turmaDisciplinaId { get; set; }
    }

    public class MaterialRequestDTO
    {
        public string titulo { get; set; }
        public string tipo { get; set; }
        public string? url { get; set; }
        public int topicoId { get; set; }
    }
}