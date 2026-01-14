using edu_connect_backend.DTO;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/eventos")]
    [Authorize]
    public class EventoController : ControllerBase
    {
        private readonly EventoService service;

        public EventoController(EventoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetEventos([FromQuery] int mes, [FromQuery] int ano)
        {
            try
            {
                // Validação básica
                if (mes < 1 || mes > 12 || ano < 2000)
                    return BadRequest("Mês ou ano inválidos.");

                var eventos = service.ListarEventos(mes, ano);
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Coordenador,Admin")]
        public IActionResult CriarEvento([FromBody] EventoRequestDTO dto)
        {
            try
            {
                service.CriarEvento(dto);
                return Created("", "Evento criado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}