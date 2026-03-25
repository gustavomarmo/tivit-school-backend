namespace edu_connect_backend.Application.DTOs
{
    public class ProfessorResponseDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string matricula { get; set; }
        public string especialidade { get; set; }
        public bool ativo { get; set; }
    }
}