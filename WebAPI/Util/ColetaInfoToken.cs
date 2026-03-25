using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace edu_connect_backend.WebAPI.Util
{
    public class ColetaInfoToken
    {
        public static int ObterIdUsuarioLogado(HttpContext httpContext)
        {
            var idClaim = httpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id)) return id;
            throw new Exception("Usuário não identificado.");
        }

        public static string ObterEmailUsuarioLogado(HttpContext httpContext)
        {
            var emailClaim = httpContext?.User.FindFirst(ClaimTypes.Email);
            if (emailClaim != null) return emailClaim.Value;
            throw new Exception("E-mail do usuário não identificado.");
        }

        public static string ObterNomeAlunoLogado(HttpContext httpContext)
        {
            var nomeClaim = httpContext?.User.FindFirst(ClaimTypes.Name);
            if (nomeClaim != null) return nomeClaim.Value;
            throw new Exception("Nome do usuário não identificado.");
        }
    }
}
