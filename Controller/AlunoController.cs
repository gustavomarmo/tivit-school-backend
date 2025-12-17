using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("aluno")]
    [Authorize]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService service;

        public AlunoController(AlunoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetAlunos([FromQuery] string? busca)
        {
            var resultado = service.ListarAlunos(busca);
            return Ok(resultado);
        }
    }
}