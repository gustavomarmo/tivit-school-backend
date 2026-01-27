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
        private readonly AcademicoService academicoService;

        public TurmaController(AcademicoService academicoService)
        {
            this.academicoService = academicoService;
        }

        [HttpGet]
        [Authorize(Roles = "Coordenador")]
        public IActionResult ListarTurmas()
        {
            var resultado = academicoService.ListarTurmas();
            return Ok(resultado);
        }
    }
}
