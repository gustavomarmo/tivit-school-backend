using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO
{
    public class ProfessorRequestDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string nome { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória")]
        public string matricula { get; set; }
        public string email { get; set; }
        public string especialidade { get; set; }
        public bool ativo { get; set; } = true;
    }
}