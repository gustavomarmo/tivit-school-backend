using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/notas")]
    [Authorize]
    public class NotaController : ControllerBase
    {
        private readonly NotaService _service;

        public NotaController(NotaService service)
        {
            _service = service;
        }

        [HttpGet("boletim")]
        [Authorize(Roles = "Aluno")]
        public IActionResult GetBoletim()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var boletim = _service.ObterBoletim(email);

            if (boletim == null)
                return NotFound("Aluno não encontrado ou sem notas registradas.");

            return Ok(boletim);
        }

        [HttpGet("lancamento")]
        [Authorize(Roles = "Professor, Coordenador")]
        public IActionResult GetLancamento([FromQuery] int turmaId, [FromQuery] int disciplinaId, [FromQuery] int bimestre)
        {
            if (turmaId <= 0 || disciplinaId <= 0 || bimestre <= 0)
                return BadRequest("Parâmetros inválidos.");

            var lista = _service.ObterListaLancamento(turmaId, disciplinaId, bimestre);

            return Ok(lista);
        }
    }
}