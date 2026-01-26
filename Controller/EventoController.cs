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
        public IActionResult listarEventos([FromQuery] int mes, [FromQuery] int ano)
        {
            try
            {
                if (mes < 1 || mes > 12 || ano < 2000)
                    return BadRequest("Mês ou ano inválidos.");

                var eventos = service.listarEventos(mes, ano);
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Coordenador,Admin")]
        public IActionResult criarEvento([FromBody] EventoRequestDTO dto)
        {
            try
            {
                service.criarEvento(dto);
                return Created("", "Evento criado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult editarEvento(int id, [FromBody] EventoRequestDTO dto)
        {
            try
            {
                service.editarEvento(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult deletarEvento(int id)
        {
            try
            {
                service.deletarEvento(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}