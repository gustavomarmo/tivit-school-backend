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
        private readonly AcademicoService academicoService;

        public AcademicoController(AcademicoService academicoService)
        {
            this.academicoService = academicoService;
        }

        [HttpGet("disciplinas")]
        public IActionResult ListarDisciplinas()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var resultado = academicoService.ListarDisciplinas(email!);
            return Ok(resultado);
        }

        [HttpGet("turmas")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult ListarTurmas()
        {
            try
            {
                var resultado = academicoService.ListarTurmas();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao listar turmas: " + ex.Message);
            }
        }

        [HttpGet("disciplinas/{id}/conteudo")]
        public IActionResult ObterConteudo(int id)
        {
            var conteudo = academicoService.ObterConteudo(id);
            if (conteudo == null) return NotFound("Disciplina não encontrada");
            return Ok(conteudo);
        }

        [HttpPost("topicos")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult CriarTopico([FromBody] TopicoRequestDTO dto)
        {
            academicoService.CriarTopico(dto);
            return StatusCode(201);
        }

        [HttpPut("topicos/{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult EditarTopico(int id, [FromBody] TopicoRequestDTO dto)
        {
            academicoService.EditarTopico(id, dto);
            return NoContent();
        }

        [HttpDelete("topicos/{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult DeletarTopico(int id)
        {
            academicoService.DeletarTopico(id);
            return NoContent();
        }

        [HttpPost("materiais")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult CriarMaterial([FromBody] MaterialRequestDTO dto)
        {
            academicoService.CriarMaterial(dto);
            return StatusCode(201);
        }

        [HttpPut("materiais/{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult EditarMaterial(int id, [FromBody] MaterialRequestDTO dto)
        {
            academicoService.EditarMaterial(id, dto);
            return NoContent();
        }

        [HttpDelete("materiais/{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult DeletarMaterial(int id)
        {
            academicoService.DeletarMaterial(id);
            return NoContent();
        }

        [HttpPost("atividades")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult CriarAtividade([FromBody] AtividadeRequestDTO dto)
        {
            academicoService.CriarAtividade(dto);
            return Ok("Atividade criada com sucesso!");
        }

        [HttpPut("atividades/{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult EditarAtividade(int id, [FromBody] AtividadeRequestDTO dto)
        {
            academicoService.EditarAtividade(id, dto);
            return NoContent();
        }

        [HttpDelete("atividades/{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult DeletarAtividade(int id)
        {
            academicoService.DeletarAtividade(id);
            return NoContent();
        }

        [HttpPost("atividade/entregar")]
        public async Task<IActionResult> EntregarAtividade([FromForm] EntregaAtividadeDTO dto)
        {
            if (dto.Arquivo == null || dto.Arquivo.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            return Ok("Atividade entregue com sucesso!");
        }

        [HttpPost("disciplinas")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult CriarDisciplina([FromBody] DisciplinaCriacaoDTO dto)
        {
            academicoService.CriarDisciplinaGenerica(dto);
            return StatusCode(201);
        }

        [HttpPost("disciplinas/vincular")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult VincularDisciplina([FromBody] VincularDisciplinaDTO dto)
        {
            academicoService.VincularDisciplina(dto);
            return StatusCode(201);
        }
    }
}