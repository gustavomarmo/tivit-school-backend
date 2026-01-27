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
        private readonly EventoService eventoService;

        public EventoController(EventoService eventoService)
        {
            this.eventoService = eventoService;
        }

        [HttpGet]
        public IActionResult ListarEventos([FromQuery] int mes, [FromQuery] int ano)
        {
            if (mes < 1 || mes > 12 || ano < 2000)
                return BadRequest("Mês ou ano inválidos.");

            var eventos = eventoService.ListarEventos(mes, ano);
            return Ok(eventos);
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Coordenador,Admin")]
        public IActionResult CriarEvento([FromBody] EventoRequestDTO dto)
        {
            eventoService.CriarEvento(dto);
            return Created("", "Evento criado com sucesso!");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult EditarEvento(int id, [FromBody] EventoRequestDTO dto)
        {
            eventoService.EditarEvento(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult DeletarEvento(int id)
        {
            eventoService.DeletarEvento(id);
            return NoContent();
        }
    }
}