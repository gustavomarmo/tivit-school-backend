using edu_connect_backend.DTO;
using edu_connect_backend.Mapper;
using edu_connect_backend.Service;
using edu_connect_backend.Util;
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
        private readonly ExtracurricularMapper extracurricularMapper;

        public ExtracurricularController(
            ExtracurricularService extracurricularService,
            ExtracurricularMapper mapper)
        {
            this.extracurricularService = extracurricularService;
            this.extracurricularMapper = mapper;
        }

        [HttpGet]
        public IActionResult ListarExtracurriculares()
        {
            var email = ColetaInfoToken.ObterEmailUsuarioLogado(HttpContext);
            if (email == null) return Unauthorized();

            try
            {
                var models = extracurricularService.ListarExtracurriculares(email);
                return Ok(extracurricularMapper.ToDisciplinaResumoDTOList(models));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao listar atividades: " + ex.Message });
            }
        }

        [HttpGet("{id}/conteudo")]
        public IActionResult ObterConteudo(int id)
        {
            var model = extracurricularService.ObterConteudoExtracurricular(id);
            if (model == null) return NotFound(new { message = "Atividade não encontrada." });

            var usuarioId = ColetaInfoToken.ObterIdUsuarioLogado(HttpContext);
            if (usuarioId == null) return Unauthorized();

            var dto = extracurricularMapper.ToDisciplinaConteudoDTO(model, usuarioId);

            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador")]
        public IActionResult CriarExtracurricular([FromBody] ExtracurricularRequestDTO dto)
        {
            var model = extracurricularMapper.ToExtracurricular(dto);
            extracurricularService.CriarAtividadeExtracurricular(model);

            return Created("", new { message = "Atividade criada com sucesso." });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult EditarExtracurricular(int id, [FromBody] ExtracurricularRequestDTO dto)
        {
            var model = extracurricularMapper.ToExtracurricular(dto);
            extracurricularService.EditarAtividadeExtracurricular(id, model);
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
            var model = extracurricularMapper.ToTurmaExtracurricular(dto);
            extracurricularService.VincularExtracurricular(model);

            return Created("", new { message = "Vínculo criado com sucesso." });
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