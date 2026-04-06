using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Matricula
{
    public class MatriculaPasso2RequestDTO
    {
        [Required(ErrorMessage = "O ID da solicitação é obrigatório")]
        public int SolicitacaoId { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório")]
        [MaxLength(250, ErrorMessage = "O endereço deve ter no máximo 250 caracteres")]
        public string EnderecoCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O RG é obrigatório")]
        [MaxLength(20, ErrorMessage = "RG inválido")]
        public string Rg { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome do responsável é obrigatório")]
        [MaxLength(150, ErrorMessage = "O nome do responsável deve ter no máximo 150 caracteres")]
        public string NomeResponsavel { get; set; } = string.Empty;

        [MaxLength(20, ErrorMessage = "Contato inválido")]
        public string ContatoResponsavel { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "A escolaridade deve ter no máximo 100 caracteres")]
        public string EscolaridadeAnterior { get; set; } = string.Empty;
    }
}