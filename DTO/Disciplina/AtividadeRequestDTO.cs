namespace edu_connect_backend.DTO.Disciplina
{
    public class AtividadeRequestDTO
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal NotaMaxima { get; set; }
        public int TopicoId { get; set; }
    }
}