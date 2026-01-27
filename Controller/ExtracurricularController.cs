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
        private readonly AcademicoService academicoService;

        public ExtracurricularController(AcademicoService academicoService)
        {
            this.academicoService = academicoService;
        }

        [HttpGet]
        public IActionResult ListarExtracurriculares()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            try
            {
                var resultado = academicoService.ListarExtracurriculares(email);
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
            var conteudo = academicoService.ObterConteudoExtracurricular(id);
            if (conteudo == null) return NotFound("Atividade não encontrada.");
            return Ok(conteudo);
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador")]
        public IActionResult CriarExtracurricular([FromBody] ExtracurricularRequestDTO dto)
        {
            academicoService.CriarAtividadeExtracurricular(dto);
            return StatusCode(201, "Atividade criada com sucesso.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult EditarExtracurricular(int id, [FromBody] ExtracurricularRequestDTO dto)
        {
            academicoService.EditarAtividadeExtracurricular(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult DeletarExtracurricular(int id)
        {
            academicoService.DeletarAtividadeExtracurricular(id);
            return NoContent();
        }

        [HttpPost("vincular")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult VincularExtracurricular([FromBody] VincularExtracurricularDTO dto)
        {
            academicoService.VincularExtracurricular(dto);
            return StatusCode(201);
        }

        [HttpDelete("vinculo/{id}")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult RemoverVinculoExtracurricular(int id)
        {
            academicoService.RemoverVinculoExtracurricular(id);
            return NoContent();
        }
    }
}