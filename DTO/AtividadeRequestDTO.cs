namespace edu_connect_backend.DTO
{
    public class AtividadeRequestDTO
    {
        public string titulo { get; set; }
        public string descricao { get; set; }
        public DateTime dataEntrega { get; set; }
        public decimal notaMaxima { get; set; }
        public int topicoId { get; set; }
    }
}