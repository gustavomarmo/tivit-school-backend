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
            usuarioService.CadastrarUsuario(usuario);
            return Ok(new { message = "Usuário cadastrado com sucesso!" });
        }

        [HttpPost("esqueci-senha")]
        public async Task<IActionResult> EsqueciSenha([FromBody] EsqueciSenhaRequestDTO dto)
        {
            await usuarioService.SolicitarResetSenhaAsync(dto.Email);
            
            return Ok(new { message = "Se o e-mail estiver cadastrado em nossa base de dados, um código de verificação foi enviado." });
        }

        [HttpPost("validar-otp")]
        public IActionResult ValidarOtp([FromBody] ValidarOtpDTO dto)
        {
            var isValido = usuarioService.ValidarOtpSenha(dto.Email, dto.Codigo);

            if (!isValido)
                return BadRequest(new { message = "Código inválido ou expirado." });

            return Ok(new { message = "Código validado com sucesso!" });
        }

        [HttpPost("resetar-senha")]
        public IActionResult ResetarSenha([FromBody] ResetarSenhaRequestDTO dto)
        {
            var isSucesso = usuarioService.ResetarSenha(dto.Email, dto.Codigo, dto.NovaSenha);

            if (!isSucesso)
                return BadRequest(new { message = "Não foi possível redefinir a senha. Código inválido ou expirado." });

            return Ok(new { message = "Senha redefinida com sucesso!" });
        }
    }
}