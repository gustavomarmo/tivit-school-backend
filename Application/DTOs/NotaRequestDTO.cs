namespace edu_connect_backend.Application.DTOs
{
    public class NotaRequestDTO
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public int DisciplinaId { get; set; }
        public int Bimestre { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}