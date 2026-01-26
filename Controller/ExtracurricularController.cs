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
        private readonly AcademicoService service;

        public ExtracurricularController(AcademicoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult listarExtracurriculares()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            try
            {
                var resultado = service.listarExtracurriculares(email);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao listar atividades: " + ex.Message);
            }
        }

        [HttpGet("{id}/conteudo")]
        public IActionResult obterConteudo(int id)
        {
            try
            {
                var conteudo = service.obterConteudoExtracurricular(id);
                if (conteudo == null) return NotFound("Atividade não encontrada.");
                return Ok(conteudo);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao obter conteúdo: " + ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador")]
        public IActionResult criarExtracurricular([FromBody] ExtracurricularRequestDTO dto)
        {
            try
            {
                service.criarAtividadeExtracurricular(dto);
                return StatusCode(201, "Atividade criada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult editarExtracurricular(int id, [FromBody] ExtracurricularRequestDTO dto)
        {
            try
            {
                service.editarAtividadeExtracurricular(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult deletarExtracurricular(int id)
        {
            try
            {
                service.deletarAtividadeExtracurricular(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("vincular")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult vincularExtracurricular([FromBody] VincularExtracurricularDTO dto)
        {
            try
            {
                service.vincularExtracurricular(dto);
                return StatusCode(201, "Atividade vinculada à turma com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("vinculo/{id}")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult removerVinculoExtracurricular(int id)
        {
            try
            {
                service.removerVinculoExtracurricular(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}