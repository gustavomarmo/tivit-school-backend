using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.Application.DTOs
{
    public class MatriculaPasso2DTO
    {
        [Required]
        public int SolicitacaoId { get; set; }

        [Required]
        public string EnderecoCompleto { get; set; } = string.Empty;

        [Required]
        public string Rg { get; set; } = string.Empty;

        [Required]
        public string NomeResponsavel { get; set; } = string.Empty;

        public string ContatoResponsavel { get; set; } = string.Empty;

        public string EscolaridadeAnterior { get; set; } = string.Empty;
    }
}