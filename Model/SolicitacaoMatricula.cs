using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.Model
{
    [Table("solicitacao_matricula")]
    public class SolicitacaoMatricula
    {
        [Key]
        public int id { get; set; }

        // --- Passo 1: Dados Iniciais ---
        [Required]
        public string nomeCompleto { get; set; } = string.Empty;

        [Required]
        public string cpf { get; set; } = string.Empty;

        [Required]
        public string email { get; set; } = string.Empty;

        public string telefone { get; set; } = string.Empty;

        public DateTime dataNascimento { get; set; }

        // --- Controle de Segurança (OTP) ---
        public string? codigoOtp { get; set; }
        public DateTime? validadeOtp { get; set; }
        public string? enderecoCompleto { get; set; }
        public string? rg { get; set; }
        public string? nomeResponsavel { get; set; }
        public string? contatoResponsavel { get; set; }
        public string? escolaridadeAnterior { get; set; }

        public string? serieDesejada { get; set; }
        public Turno? turnoDesejado { get; set; }
        public decimal? valorMensalidade { get; set; }

        public StatusMatricula status { get; set; } = StatusMatricula.Iniciado;
        public DateTime dataSolicitacao { get; set; } = DateTime.Now;

        public List<DocumentoMatricula> documentos { get; set; } = new();
    }
}