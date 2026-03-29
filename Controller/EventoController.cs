using edu_connect_backend.DTO;
using edu_connect_backend.Mapper;
using edu_connect_backend.Service;
using edu_connect_backend.Util;
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
        private readonly EventoMapper eventoMapper;

        public EventoController(EventoService eventoService, EventoMapper eventoMapper)
        {
            this.eventoService = eventoService;
            this.eventoMapper = eventoMapper;
        }

        [HttpGet]
        public IActionResult ListarEventos([FromQuery] int mes, [FromQuery] int ano)
        {
            if (mes < 1 || mes > 12 || ano < 2000)
                return BadRequest(new { message = "Mês ou ano inválidos." });

            var eventosModel = eventoService.ListarEventos(mes, ano);
            var eventosDTO = eventoMapper.ToEventoResponseDTOList(eventosModel);

            return Ok(eventosDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Coordenador,Admin")]
        public IActionResult CriarEvento([FromBody] EventoRequestDTO dto)
        {
            var usuarioId = ColetaInfoToken.ObterIdUsuarioLogado(HttpContext);
            if (usuarioId == null) return Unauthorized();

            var evento = eventoMapper.ToEvento(dto);
            evento.usuarioCriadorId = usuarioId;

            eventoService.CriarEvento(evento);

            return Created("", new { message = "Evento criado com sucesso!" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult EditarEvento(int id, [FromBody] EventoRequestDTO dto)
        {
            var evento = eventoMapper.ToEvento(dto);
            eventoService.EditarEvento(id, evento);
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