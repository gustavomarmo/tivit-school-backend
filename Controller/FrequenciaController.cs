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
        private readonly FrequenciaService service;

        public FrequenciaController(FrequenciaService service)
        {
            this.service = service;
        }

        [HttpPost("chamada")]
        [Authorize(Roles = "Professor")]
        public IActionResult RealizarChamada([FromBody] ChamadaRequestDTO dto)
        {
            try
            {
                service.RealizarChamada(dto);
                return Ok("Chamada registrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}