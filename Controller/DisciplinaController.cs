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
    [Route("api/disciplinas")]
    [Authorize]
    public class DisciplinaController : ControllerBase
    {
        private readonly DisciplinaService disciplinaService;
        private readonly DisciplinaMapper disciplinaMapper;

        public DisciplinaController(DisciplinaService disciplinaService, DisciplinaMapper disciplinaMapper)
        {
            this.disciplinaService = disciplinaService;
            this.disciplinaMapper = disciplinaMapper;
        }

        [HttpPost("criar")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult CriarDisciplina([FromBody] DisciplinaCriacaoDTO dto)
        {
            disciplinaService.CriarDisciplinaGenerica(disciplinaMapper.ToDisciplina(dto));
            return Ok(new { message = "Disciplina criada com sucesso." });
        }

        [HttpPost("vincular")]
        [Authorize(Roles = "Coordenador")]
        public IActionResult VincularDisciplina([FromBody] VincularDisciplinaDTO dto)
        {
            disciplinaService.VincularDisciplina(disciplinaMapper.ToTurmaDisciplina(dto));
            return Ok(new { message = "Disciplina vinculada à turma." });
        }

        [HttpGet("listar")]
        public IActionResult ListarDisciplinas()
        {
            var email = ColetaInfoToken.ObterEmailUsuarioLogado(HttpContext);
            if (email == null) return Unauthorized();

            var dtos = disciplinaMapper.ToDisciplinaResumoDTOList(disciplinaService.ListarDisciplinas(email));

            return Ok(dtos);
        }

        [HttpGet("{id}/conteudo")]
        public IActionResult ObterConteudoDisciplina(int id)
        {
            var email = ColetaInfoToken.ObterEmailUsuarioLogado(HttpContext);
            var (modelo, entregues) = disciplinaService.ObterConteudoDisciplina(id, email);

            if (modelo == null) return NotFound("Disciplina não encontrada.");

            return Ok(disciplinaMapper.ToDisciplinaConteudoDTO(modelo, entregues));
        }

        [HttpGet]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult ListarTodasDisciplinas()
        {
            var disciplinas = disciplinaService.ListarTodasDisciplinasGenericas();
            return Ok(disciplinas);
        }
    }
}