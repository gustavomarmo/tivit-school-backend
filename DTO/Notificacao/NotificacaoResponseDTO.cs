namespace edu_connect_backend.DTO.Notificacao
{
    public class NotificacaoResponseDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Tempo { get; set; }
        public bool Lido { get; set; }
        public long Timestamp { get; set; }
    }
}