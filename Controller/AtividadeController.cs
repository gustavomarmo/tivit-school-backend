using edu_connect_backend.DTO;
using edu_connect_backend.Mapper;
using edu_connect_backend.Service;
using edu_connect_backend.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/atividades")]
    [Authorize]
    public class AtividadeController : ControllerBase
    {
        private readonly AtividadeService atividadeService;
        private readonly AtividadeMapper atividadeMapper;

        public AtividadeController(AtividadeService atividadeService, AtividadeMapper atividadeMapper)
        {
            this.atividadeService = atividadeService;
            this.atividadeMapper = atividadeMapper;
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult CriarAtividade([FromBody] AtividadeRequestDTO dto)
        {
            atividadeService.CriarAtividade(atividadeMapper.ToMaterial(dto));
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult EditarAtividade(int id, [FromBody] AtividadeRequestDTO dto)
        {
            atividadeService.EditarAtividade(id, atividadeMapper.ToMaterial(dto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult DeletarAtividade(int id)
        {
            atividadeService.DeletarAtividade(id);
            return NoContent();
        }

        [HttpPost("entregar")]
        public async Task<IActionResult> EntregarAtividade([FromForm] EntregaAtividadeDTO dto)
        {
            if (dto.Arquivo == null || dto.Arquivo.Length == 0)
                return BadRequest(new { message = "Nenhum arquivo enviado." });

            var usuarioId = ColetaInfoToken.ObterIdUsuarioLogado(HttpContext);
            if (usuarioId == null) return Unauthorized();

            atividadeService.RegistrarEntrega(dto.AtividadeId, usuarioId, dto.Arquivo);

            return Ok(new { messsage = "Atividade entregue com sucesso!" });
        }
    }
}