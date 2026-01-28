using edu_connect_backend.DTO;
using edu_connect_backend.Mapper;
using edu_connect_backend.Model;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("auth")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService usuarioService;
        private readonly TokenService tokenService;
        private readonly UsuarioMapper usuarioMapper;

        public UsuarioController(
            UsuarioService usuarioService,
            TokenService tokenService,
            UsuarioMapper usuarioMapper)
        {
            this.usuarioService = usuarioService;
            this.tokenService = tokenService;
            this.usuarioMapper = usuarioMapper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO loginDto)
        {
            var usuario = usuarioService.Autenticar(loginDto.email, loginDto.senha);

            if (usuario == null)
                return Unauthorized(new { message = "Email ou senha inválidos." });

            var token = tokenService.gerarToken(usuario);

            var response = usuarioMapper.ToLoginResponseDTO(usuario, token);

            return Ok(response);
        }

        [HttpPost("cadastro")]
        public IActionResult CadastrarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                usuarioService.CadastrarUsuario(usuario);
                return Ok(new { message = "Usuário cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}