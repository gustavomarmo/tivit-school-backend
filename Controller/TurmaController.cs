using edu_connect_backend.Mapper;
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
        private readonly TurmaMapper turmaMapper;

        public TurmaController(TurmaService turmaService, TurmaMapper turmaMapper)
        {
            this.turmaService = turmaService;
            this.turmaMapper = turmaMapper;
        }

        [HttpGet]
        [Authorize(Roles = "Coordenador")]
        public IActionResult ListarTurmas()
        {
            var turmasModel = turmaService.ListarTurmas();
            var resultado = turmaMapper.ToTurmaResponseDTOList(turmasModel);
            return Ok(resultado);
        }
    }
}