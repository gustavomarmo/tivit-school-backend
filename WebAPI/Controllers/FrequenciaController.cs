using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Application.Mappers;
using edu_connect_backend.Application.Services;
using edu_connect_backend.WebAPI.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/frequencias")]
    [Authorize]
    public class FrequenciaController : ControllerBase
    {
        private readonly FrequenciaService frequenciaService;
        private readonly FrequenciaMapper frequenciaMapper;

        public FrequenciaController(FrequenciaService frequenciaService, FrequenciaMapper frequenciaMapper)
        {
            this.frequenciaService = frequenciaService;
            this.frequenciaMapper = frequenciaMapper;
        }

        [HttpPost("chamada")]
        [Authorize(Roles = "Professor")]
        public IActionResult RealizarChamada([FromBody] ChamadaRequestDTO dto)
        {
            var frequencias = frequenciaMapper.ToFrequenciaList(dto);

            frequenciaService.RealizarChamada(frequencias);

            return Ok(new { message = "Chamada registrada com sucesso!" });
        }

        [HttpGet("resumo")]
        [Authorize(Roles = "Aluno")]
        public IActionResult ObterResumoFrequencia()
        {
            var usuarioId = ColetaInfoToken.ObterIdUsuarioLogado(HttpContext);
            if (usuarioId == null) return Unauthorized();

            var resumoModel = frequenciaService.ObterResumoFrequencia(usuarioId);

            var resumoDTO = frequenciaMapper.ToFrequenciaResumoDTOList(resumoModel);

            return Ok(resumoDTO);
        }
    }
}