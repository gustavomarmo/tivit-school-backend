using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Disciplina
{
    public class VincularDisciplinaRequestDTO
    {
        [Required(ErrorMessage = "O ID da turma é obrigatório")]
        public int TurmaId { get; set; }

        [Required(ErrorMessage = "O ID da disciplina é obrigatório")]
        public int DisciplinaId { get; set; }

        [Required(ErrorMessage = "O ID do professor é obrigatório")]
        public int ProfessorId { get; set; }
    }
}
