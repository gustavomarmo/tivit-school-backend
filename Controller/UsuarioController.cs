using edu_connect_backend.Model;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService service;

        public UsuarioController(UsuarioService service)
        {
            this.service = service;
        }

        [HttpPost(Name = "GetUsuario")]
        public IActionResult CadastrarUsuario([FromBody] Usuario usuario)
        {
            service.CadastrarUsuario(usuario);
            return Ok("Usuário cadastrado com sucesso!");
        }
    }
}
