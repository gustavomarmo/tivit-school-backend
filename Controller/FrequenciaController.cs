using edu_connect_backend.DTO;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/frequencia")]
    [Authorize]
    public class FrequenciaController : ControllerBase
    {
        private readonly FrequenciaService frequenciaService;

        public FrequenciaController(FrequenciaService frequenciaService)
        {
            this.frequenciaService = frequenciaService;
        }

        [HttpPost("chamada")]
        [Authorize(Roles = "Professor")]
        public IActionResult RealizarChamada([FromBody] ChamadaRequestDTO dto)
        {
            frequenciaService.RealizarChamada(dto);
            return Ok("Chamada registrada com sucesso!");
        }

        [HttpGet("resumo")]
        [Authorize(Roles = "Aluno")]
        public IActionResult ObterResumoFrequencia()
        {
            var resumo = frequenciaService.ObterResumoFrequenciaLogado();
            return Ok(resumo);
        }
    }
}