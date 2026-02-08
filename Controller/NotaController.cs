using edu_connect_backend.DTO;
using edu_connect_backend.Mapper;
using edu_connect_backend.Service;
using edu_connect_backend.Util;
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
        private readonly NotaService notaService;
        private readonly BoletimPdfService pdfService;
        private readonly NotaMapper notaMapper;

        public NotaController(NotaService notaService, BoletimPdfService pdfService, NotaMapper notaMapper)
        {
            this.notaService = notaService;
            this.pdfService = pdfService;
            this.notaMapper = notaMapper;
        }

        [HttpGet("boletim")]
        [Authorize(Roles = "Aluno")]
        public IActionResult ObterBoletim()
        {
            string email = ColetaInfoToken.ObterEmailUsuarioLogado(HttpContext);
            if (email == null) return Unauthorized();

            var boletimModel = notaService.obterBoletim(email);

            if (boletimModel == null)
                return NotFound(new { message = "Aluno não encontrado ou sem notas registradas." });

            return Ok(notaMapper.ToBoletimDTOList(boletimModel));
        }

        [HttpGet("lancamento")]
        [Authorize(Roles = "Professor, Coordenador")]
        public IActionResult ObterListaFrequencia([FromQuery] int turmaId, [FromQuery] int disciplinaId, [FromQuery] int bimestre)
        {
            if (turmaId <= 0 || disciplinaId <= 0 || bimestre <= 0)
                return BadRequest(new { message = "Parâmetros inválidos." });

            var listaModel = notaService.obterListaLancamento(turmaId, disciplinaId, bimestre);

            return Ok(notaMapper.ToNotaLancamentoDTOList(listaModel));
        }

        [HttpPost("lote")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult salvarNotasLote([FromBody] List<NotaRequestDTO> notasDto)
        {
            try
            {
                if (notasDto == null || notasDto.Count == 0)
                    return BadRequest(new { message = "A lista de notas está vazia." });

                var notasModel = notaMapper.ToNotaList(notasDto);
                notaService.lancarNotasEmLote(notasModel);

                return Ok(new { mensagem = $"{notasDto.Count} notas processadas com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao salvar lote: {ex.Message}" });
            }
        }

        [HttpGet("boletim/download")]
        [Authorize(Roles = "Aluno")]
        public IActionResult downloadBoletim()
        {
            var email = ColetaInfoToken.ObterEmailUsuarioLogado(HttpContext);
            if (email == null) return Unauthorized();

            var boletimModel = notaService.obterBoletim(email);
            var nomeAluno = ColetaInfoToken.ObterNomeAlunoLogado(HttpContext) ?? "Aluno";

            if (boletimModel == null || !boletimModel.Any())
                return NotFound(new { message = "Sem dados para gerar o boletim." });

            var boletimDto = notaMapper.ToBoletimDTOList(boletimModel);
            var pdfBytes = pdfService.gerarPdfBoletim(boletimDto, nomeAluno);

            return File(pdfBytes, "application/pdf", "meu_boletim.pdf");
        }
    }
}