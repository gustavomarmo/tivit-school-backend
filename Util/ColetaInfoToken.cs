using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace edu_connect_backend.Util
{
    public class ColetaInfoToken
    {
        public static int ObterIdUsuarioLogado(HttpContext httpContext)
        {
            var idClaim = httpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id)) return id;
            throw new Exception("Usuário não identificado.");
        }
    }
}
