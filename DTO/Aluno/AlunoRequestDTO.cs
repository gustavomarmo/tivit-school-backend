using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Aluno
{
    public class AlunoRequestDTO
    {
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string Email { get; set; }
        public int? TurmaId { get; set; }
        public bool Ativo { get; set; } = true;

    }
}
