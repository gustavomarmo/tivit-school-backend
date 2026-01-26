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
        private readonly NotaService service;
        private readonly BoletimPdfService pdfService;

        public NotaController(NotaService service, BoletimPdfService pdfService)
        {
            this.service = service;
            this.pdfService = pdfService;
        }

        [HttpGet("boletim")]
        [Authorize(Roles = "Aluno")]
        public IActionResult obterBoletim()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var boletim = service.obterBoletim(email);

            if (boletim == null)
                return NotFound("Aluno não encontrado ou sem notas registradas.");

            return Ok(boletim);
        }

        [HttpGet("lancamento")]
        [Authorize(Roles = "Professor, Coordenador")]
        public IActionResult obterListaFrequencia([FromQuery] int turmaId, [FromQuery] int disciplinaId, [FromQuery] int bimestre)
        {
            if (turmaId <= 0 || disciplinaId <= 0 || bimestre <= 0)
                return BadRequest("Parâmetros inválidos.");

            var lista = service.obterListaLancamento(turmaId, disciplinaId, bimestre);

            return Ok(lista);
        }

        [HttpPost("lote")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult salvarNotasLote([FromBody] List<NotaRequestDTO> notas)
        {
            try
            {
                if (notas == null || notas.Count == 0)
                    return BadRequest("A lista de notas está vazia.");

                service.lancarNotasEmLote(notas);

                return Ok(new { mensagem = $"{notas.Count} notas processadas com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao salvar lote: {ex.Message}");
            }
        }

        [HttpGet("boletim/download")]
        [Authorize(Roles = "Aluno")]
        public IActionResult downloadBoletim()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var boletim = service.obterBoletim(email);
            var nomeAluno = User.FindFirst(ClaimTypes.Name)?.Value ?? "Aluno";

            if (boletim == null || !boletim.Any())
                return NotFound("Sem dados para gerar o boletim.");

            var pdfBytes = pdfService.gerarPdfBoletim(boletim, nomeAluno);

            return File(pdfBytes, "application/pdf", "meu_boletim.pdf");
        }
    }
}