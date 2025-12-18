using edu_connect_backend.DTO;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("academico")]
    [Authorize]
    public class AcademicoController : ControllerBase
    {
        private readonly AcademicoService service;

        public AcademicoController(AcademicoService service)
        {
            this.service = service;
        }

        [HttpGet("disciplinas/minhas")]
        public IActionResult GetMinhasDisciplinas()
        {
            // Pega o email de dentro do token automaticamente
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var resultado = service.ListarMinhasDisciplinas(email!);
            return Ok(resultado);
        }

        [HttpGet("disciplinas/{id}/conteudo")]
        public IActionResult GetConteudo(int id)
        {
            var conteudo = service.ObterConteudo(id);
            if (conteudo == null) return NotFound("Disciplina não encontrada");
            return Ok(conteudo);
        }

        [HttpPost("topicos")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult PostTopico([FromBody] TopicoRequestDTO dto)
        {
            service.CriarTopico(dto);
            return StatusCode(201);
        }

        [HttpPost("materiais")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult PostMaterial([FromBody] MaterialRequestDTO dto)
        {
            service.CriarMaterial(dto);
            return StatusCode(201);
        }

        [HttpPost("atividades/{id}/entrega")]
        [Authorize(Roles = "Aluno")]
        public IActionResult PostEntrega(int id, [FromBody] object entrega)
        {
            // Endpoint mockado para aceitar a chamada do front
            return Ok("Entrega recebida com sucesso");
        }

        [HttpPost("disciplinas")]
        [Authorize(Roles = "Coordenador")] // Só coordenador cadastra matéria nova
        public IActionResult CriarDisciplina([FromBody] DisciplinaCriacaoDTO dto)
        {
            this.service.CriarDisciplinaGenerica(dto);
            return StatusCode(201);
        }

        [HttpPost("disciplinas/vincular")]
        [Authorize(Roles = "Coordenador")] // Só coordenador monta a grade
        public IActionResult Vincular([FromBody] VincularDisciplinaDTO dto)
        {
            this.service.VincularDisciplina(dto);
            return StatusCode(201);
        }
    }
}