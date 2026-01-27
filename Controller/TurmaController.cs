using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/turmas")]
    [Authorize]
    public class TurmaController : ControllerBase
    {
        private readonly TurmaService turmaService;

        public TurmaController(TurmaService turmaService)
        {
            this.turmaService = turmaService;
        }

        [HttpGet]
        [Authorize(Roles = "Coordenador")]
        public IActionResult ListarTurmas()
        {
            var resultado = turmaService.ListarNomesTurmas();
            return Ok(resultado);
        }
    }
}
