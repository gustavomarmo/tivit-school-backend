using edu_connect_backend.DTO;
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
        private readonly BoletimPdfService _pdfService;

        public NotaController(NotaService service, BoletimPdfService pdfService)
        {
            _service = service;
            _pdfService = pdfService;
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

        [HttpPost("lote")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult SalvarNotasLote([FromBody] List<NotaRequestDTO> notas)
        {
            try
            {
                if (notas == null || notas.Count == 0)
                    return BadRequest("A lista de notas está vazia.");

                _service.LancarNotasEmLote(notas);

                return Ok(new { mensagem = $"{notas.Count} notas processadas com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao salvar lote: {ex.Message}");
            }
        }

        [HttpGet("boletim/download")]
        [Authorize(Roles = "Aluno")]
        public IActionResult DownloadBoletim()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var boletim = _service.ObterBoletim(email);
            var nomeAluno = User.FindFirst(ClaimTypes.Name)?.Value ?? "Aluno";

            if (boletim == null || !boletim.Any())
                return NotFound("Sem dados para gerar o boletim.");

            var pdfBytes = _pdfService.GerarPdfBoletim(boletim, nomeAluno);

            return File(pdfBytes, "application/pdf", "meu_boletim.pdf");
        }
    }
}