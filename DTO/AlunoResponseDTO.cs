namespace edu_connect_backend.DTO
{
    public class AlunoResponseDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string matricula { get; set; }
        public string? turma { get; set; }
        public int? turmaId { get; set; }
        public bool ativo { get; set; }
    }
}
