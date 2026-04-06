using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Matricula
{
    public class SelecaoVagaRequestDTO
    {
        [Required(ErrorMessage = "O ID da solicitação é obrigatório")]
        public int SolicitacaoId { get; set; }

        [Required(ErrorMessage = "A série é obrigatória")]
        [MaxLength(50, ErrorMessage = "A série deve ter no máximo 50 caracteres")]
        public string Serie { get; set; } = string.Empty;

        [Required(ErrorMessage = "O turno é obrigatório")]
        [MaxLength(20, ErrorMessage = "O turno deve ter no máximo 20 caracteres")]
        public string Turno { get; set; } = string.Empty;
    }
}