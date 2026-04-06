using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Nota
{
    public class NotaRequestDTO
    {
        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "O ID da turma é obrigatório")]
        public int TurmaId { get; set; }

        [Required(ErrorMessage = "O ID da disciplina é obrigatório")]
        public int DisciplinaId { get; set; }

        [Required(ErrorMessage = "O bimestre é obrigatório")]
        [Range(1, 2, ErrorMessage = "O bimestre deve ser 1 ou 2")]
        public int Bimestre { get; set; }

        [Required(ErrorMessage = "O tipo da nota é obrigatório")]
        [MaxLength(10, ErrorMessage = "O tipo deve ter no máximo 10 caracteres")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor da nota é obrigatório")]
        [Range(0, 10, ErrorMessage = "A nota deve ser entre 0 e 10")]
        public decimal Valor { get; set; }
    }
}