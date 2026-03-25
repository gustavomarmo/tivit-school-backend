using edu_connect_backend.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Domain.Entities
{

    [Table("usuario")]
    public class Usuario
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senhaHash { get; set; } = string.Empty;
        public string cpf { get; set; } = string.Empty;
        public PerfilUsuario perfil { get; set; }
        public string? fotoUrl { get; set; }
        public bool ativo { get; set; } = true;
        public DateTime dataCadastro;

        public string? codigoOtp { get; set; }
        public DateTime? validadeOtp { get; set; }
    }
}
