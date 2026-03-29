using edu_connect_backend.DTO;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class UsuarioMapper
    {
        public UsuarioMapper()
        {
        }
        public LoginResponseDTO ToLoginResponseDTO(Usuario usuario, string token)
        {
            return new LoginResponseDTO
            {
                email = usuario.email,
                nome = usuario.nome,
                perfil = usuario.perfil.ToString(),
                fotoUrl = usuario.fotoUrl,
                token = token
            };
        }
    }
}