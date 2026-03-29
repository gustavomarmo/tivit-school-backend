using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.Model
{
    [Table("documento_matricula")]
    public class DocumentoMatricula
    {
        [Key]
        public int id { get; set; }

        public int solicitacaoMatriculaId { get; set; }

        [ForeignKey("solicitacaoMatriculaId")]
        public SolicitacaoMatricula solicitacao { get; set; } = null!;

        public TipoDocumentoMatricula tipo { get; set; }

        [Required]
        public string caminhoArquivo { get; set; } = string.Empty; // URL

        public string nomeOriginalArquivo { get; set; } = string.Empty;

        public bool validado { get; set; } = false;

        public string? observacaoRejeicao { get; set; }
    }
}