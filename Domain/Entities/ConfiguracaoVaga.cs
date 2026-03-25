using edu_connect_backend.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Domain.Entities
{
    [Table("configuracao_vaga")]
    public class ConfiguracaoVaga
    {
        [Key]
        public int id { get; set; }

        public string serie { get; set; } = string.Empty;

        public Turno turno { get; set; }

        public decimal valorMensalidade { get; set; }

        public int vagasTotais { get; set; }

        public int anoLetivo { get; set; }
    }
}