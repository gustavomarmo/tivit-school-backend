using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Professor
{
    public class ProfessorRequestDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória")]
        public string Matricula { get; set; }
        public string Email { get; set; }
        public string Especialidade { get; set; }
        public bool Ativo { get; set; } = true;
    }
}