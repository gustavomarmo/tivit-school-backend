namespace edu_connect_backend.DTO.Aluno
{
    public class AlunoResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Matricula { get; set; }
        public string? Turma { get; set; }
        public int? TurmaId { get; set; }
        public bool Ativo { get; set; }
    }
}
