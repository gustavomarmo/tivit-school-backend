using edu_connect_backend.DTO.Auth;
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
                Email = usuario.email,
                Nome = usuario.nome,
                Perfil = usuario.perfil.ToString(),
                FotoUrl = usuario.fotoUrl,
                Token = token
            };
        }
    }
}