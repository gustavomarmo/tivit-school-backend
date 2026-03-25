using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Domain.Entities;

namespace edu_connect_backend.Application.Mappers
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