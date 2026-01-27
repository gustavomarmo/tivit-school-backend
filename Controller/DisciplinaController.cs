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
        private readonly DisciplinaService disciplinaService;

        public DisciplinaController(TopicoService academicoService)
        {
            this.disciplinaService = disciplinaService;
        }

        [HttpGet]
        public IActionResult ListarDisciplinas()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var resultado = disciplinaService.ListarDisciplinas(email);
            return Ok(resultado);
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador")]
        public IActionResult CriarDisciplina([FromBody] DisciplinaCriacaoDTO dto)
        {
            disciplinaService.CriarDisciplinaGenerica(dto);
            return StatusCode(201);
        }

        [HttpPost("vincular")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult VincularDisciplina([FromBody] VincularDisciplinaDTO dto)
        {
            disciplinaService.VincularDisciplina(dto);
            return StatusCode(201);
        }

        [HttpGet("{id}/conteudo")]
        public IActionResult ObterConteudo(int id)
        {
            var conteudo = disciplinaService.ObterConteudo(id);
            if (conteudo == null) return NotFound("Disciplina não encontrada");
            return Ok(conteudo);
        }
    }
}
