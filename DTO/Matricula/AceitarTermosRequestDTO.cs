using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Matricula
{
    public class AceitarTermosRequestDTO
    {
        [Required(ErrorMessage = "O ID da solicitação é obrigatório")]
        public int SolicitacaoId { get; set; }

        [Required(ErrorMessage = "Os termos precisam ser aceitos")]
        public bool TermosAceitos { get; set; }
    }
}
