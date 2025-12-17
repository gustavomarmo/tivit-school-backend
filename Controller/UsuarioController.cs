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
        private readonly UsuarioService service;
        private readonly TokenService tokenService;

        public UsuarioController(UsuarioService service, TokenService tokenService)
        {
            this.service = service;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO login)
        {
            var usuario = service.ObterUsuarioPorEmail(login.email);

            if (usuario == null)
                return Unauthorized("Email ou senha inválidos.");

            if (usuario.senhaHash != login.senha)
                return Unauthorized("Email ou senha inválidos.");

            var token = tokenService.GerarToken(usuario);

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
        public IActionResult CadastrarUsuario([FromBody] Usuario usuario)
        {
            service.CadastrarUsuario(usuario);
            return Ok("Usuário cadastrado com sucesso!");
        }
    }
}
