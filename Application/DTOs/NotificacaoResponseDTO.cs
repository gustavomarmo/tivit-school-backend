namespace edu_connect_backend.Application.DTOs
{
    public class NotificacaoResponseDTO
    {
        public int id { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string time { get; set; }
        public bool read { get; set; }
        public long timestamp { get; set; }
    }
}