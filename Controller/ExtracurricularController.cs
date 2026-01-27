using edu_connect_backend.DTO;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/extracurriculares")]
    [Authorize]
    public class ExtracurricularController : ControllerBase
    {
        private readonly ExtracurricularService extracurricularService;

        public ExtracurricularController(ExtracurricularService extracurricularService)
        {
            this.extracurricularService = extracurricularService;
        }

        [HttpGet]
        public IActionResult ListarExtracurriculares()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            try
            {
                var resultado = extracurricularService.ListarExtracurriculares(email);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao listar atividades: " + ex.Message);
            }
        }

        [HttpGet("{id}/conteudo")]
        public IActionResult ObterConteudo(int id)
        {
            var conteudo = extracurricularService.ObterConteudoExtracurricular(id);
            if (conteudo == null) return NotFound("Atividade não encontrada.");
            return Ok(conteudo);
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador")]
        public IActionResult CriarExtracurricular([FromBody] ExtracurricularRequestDTO dto)
        {
            extracurricularService.CriarAtividadeExtracurricular(dto);
            return StatusCode(201, "Atividade criada com sucesso.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult EditarExtracurricular(int id, [FromBody] ExtracurricularRequestDTO dto)
        {
            extracurricularService.EditarAtividadeExtracurricular(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult DeletarExtracurricular(int id)
        {
            extracurricularService.DeletarAtividadeExtracurricular(id);
            return NoContent();
        }

        [HttpPost("vincular")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult VincularExtracurricular([FromBody] VincularExtracurricularDTO dto)
        {
            extracurricularService.VincularExtracurricular(dto);
            return StatusCode(201);
        }

        [HttpDelete("vinculo/{id}")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult RemoverVinculoExtracurricular(int id)
        {
            extracurricularService.RemoverVinculoExtracurricular(id);
            return NoContent();
        }
    }
}