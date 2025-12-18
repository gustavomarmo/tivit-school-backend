namespace edu_connect_backend.DTO
{
    public class TopicoRequestDTO
    {
        public string titulo { get; set; }
        public int disciplinaId { get; set; }
    }

    public class MaterialRequestDTO
    {
        public string titulo { get; set; }
        public string tipo { get; set; }
        public string? url { get; set; }
        public int topicoId { get; set; }
    }
}