using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Frequencia
{
    public class ChamadaRequestDTO
    {
        [Required(ErrorMessage = "O ID da disciplina é obrigatório")]
        public int DisciplinaId { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Os registros são obrigatórios")]
        [MinLength(1, ErrorMessage = "É necessário ao menos um registro")]
        public List<RegistroPresencaDTO> Registros { get; set; }
    }

    public class RegistroPresencaDTO
    {
        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "A presença é obrigatória")]
        public bool Presente { get; set; }
    }
}