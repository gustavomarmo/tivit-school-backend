using edu_connect_backend.Model;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Mvc;
using edu_connect_backend.DTO;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("auth")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService usuarioService;
        private readonly TokenService tokenService;

        public UsuarioController(UsuarioService usuarioService, TokenService tokenService)
        {
            this.usuarioService = usuarioService;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] LoginRequestDTO login)
        {
            var usuario = usuarioService.obterUsuarioPorEmail(login.email);

            if (usuario == null)
                return Unauthorized("Email ou senha inválidos.");

            if (usuario.senhaHash != login.senha)
                return Unauthorized("Email ou senha inválidos.");

            var token = tokenService.gerarToken(usuario);

            return Ok(new LoginResponseDTO
            {
                email = usuario.email,
                nome = usuario.nome,
                perfil = usuario.perfil.ToString(),
                fotoUrl = usuario.fotoUrl,
                token = token
            });
        }

        [HttpPost("cadastro")]
        public IActionResult cadastrarUsuario([FromBody] Usuario usuario)
        {
            usuarioService.cadastrarUsuario(usuario);
            return Ok("Usuário cadastrado com sucesso!");
        }
    }
}
