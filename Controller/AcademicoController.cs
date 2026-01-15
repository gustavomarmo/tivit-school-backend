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

        [HttpPut("materiais/{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult EditarMaterial(int id, [FromBody] MaterialRequestDTO dto)
        {
            try
            {
                service.AtualizarMaterial(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("materiais/{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult DeletarMaterial(int id)
        {
            try
            {
                service.DeletarMaterial(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("atividades")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult CriarAtividade([FromBody] AtividadeRequestDTO dto)
        {
            try
            {
                service.CriarAtividade(dto);
                return Ok("Atividade criada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("atividades/{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult AtualizarAtividade(int id, [FromBody] AtividadeRequestDTO dto)
        {
            try
            {
                service.AtualizarAtividade(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("atividade/entregar")]
        public async Task<IActionResult> EntregarAtividade([FromForm] EntregaAtividadeDTO dto)
        {
            try
            {
                // Validações básicas
                if (dto.Arquivo == null || dto.Arquivo.Length == 0)
                    return BadRequest("Nenhum arquivo enviado.");

                return Ok("Atividade entregue com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("disciplinas")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult CriarDisciplina([FromBody] DisciplinaCriacaoDTO dto)
        {
            this.service.CriarDisciplinaGenerica(dto);
            return StatusCode(201);
        }

        [HttpPost("disciplinas/vincular")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult Vincular([FromBody] VincularDisciplinaDTO dto)
        {
            this.service.VincularDisciplina(dto);
            return StatusCode(201);
        }
    }
}