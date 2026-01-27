using edu_connect_backend.DTO;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/disciplinas")]
    [Authorize]
    public class DisciplinaController : ControllerBase
    {
        private readonly AcademicoService academicoService;

        public DisciplinaController(AcademicoService academicoService)
        {
            this.academicoService = academicoService;
        }

        [HttpGet]
        public IActionResult ListarDisciplinas()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var resultado = academicoService.ListarDisciplinas(email);
            return Ok(resultado);
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador")]
        public IActionResult CriarDisciplina([FromBody] DisciplinaCriacaoDTO dto)
        {
            academicoService.CriarDisciplinaGenerica(dto);
            return StatusCode(201);
        }

        [HttpPost("vincular")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult VincularDisciplina([FromBody] VincularDisciplinaDTO dto)
        {
            academicoService.VincularDisciplina(dto);
            return StatusCode(201);
        }

        [HttpGet("{id}/conteudo")]
        public IActionResult ObterConteudo(int id)
        {
            var conteudo = academicoService.ObterConteudo(id);
            if (conteudo == null) return NotFound("Disciplina não encontrada");
            return Ok(conteudo);
        }
    }
}
