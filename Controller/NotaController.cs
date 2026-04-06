using edu_connect_backend.DTO.Nota;
using edu_connect_backend.Mapper;
using edu_connect_backend.Service;
using edu_connect_backend.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var email = ColetaInfoToken.ObterEmailUsuarioLogado(HttpContext);
            var boletimModel = notaService.ObterBoletim(email);

            if (boletimModel == null)
                return NotFound(new { message = "Aluno não encontrado ou sem notas registradas." });

            return Ok(notaMapper.ToBoletimDTOList(boletimModel));
        }

        [HttpGet("lancamento")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult ObterListaLancamento([FromQuery] int turmaId, [FromQuery] int disciplinaId)
        {
            if (turmaId <= 0 || disciplinaId <= 0)
                return BadRequest(new { message = "Parâmetros inválidos." });

            var listaModel = notaService.ObterListaLancamento(turmaId, disciplinaId);
            return Ok(notaMapper.ToNotaLancamentoDTOList(listaModel));
        }

        [HttpPost("lote")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult SalvarNotasLote([FromBody] List<NotaRequestDTO> notasDto)
        {
            if (notasDto == null || notasDto.Count == 0)
                return BadRequest(new { message = "A lista de notas está vazia." });

            var notasModel = notaMapper.ToNotaList(notasDto);
            notaService.LancarNotasEmLote(notasModel);

            return Ok(new { mensagem = $"{notasDto.Count} notas processadas com sucesso." });
        }

        [HttpGet("boletim/download")]
        [Authorize(Roles = "Aluno")]
        public IActionResult DownloadBoletim()
        {
            var email = ColetaInfoToken.ObterEmailUsuarioLogado(HttpContext);
            var nomeAluno = ColetaInfoToken.ObterNomeAlunoLogado(HttpContext);
            var boletimModel = notaService.ObterBoletim(email);

            if (boletimModel == null || !boletimModel.Any())
                return NotFound(new { message = "Sem dados para gerar o boletim." });

            var boletimDto = notaMapper.ToBoletimDTOList(boletimModel);
            var pdfBytes = pdfService.GerarPdfBoletim(boletimDto, nomeAluno);

            return File(pdfBytes, "application/pdf", "meu_boletim.pdf");
        }
    }
}