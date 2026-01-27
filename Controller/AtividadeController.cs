using edu_connect_backend.DTO;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/atividades")]
    [Authorize]
    public class AtividadeController : ControllerBase
    {
        private readonly AcademicoService academicoService;

        public AtividadeController(AcademicoService academicoService)
        {
            this.academicoService = academicoService;
        }

        [HttpPost("atividades")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult CriarAtividade([FromBody] AtividadeRequestDTO dto)
        {
            academicoService.CriarAtividade(dto);
            return Ok("Atividade criada com sucesso!");
        }

        [HttpPut("atividades/{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult EditarAtividade(int id, [FromBody] AtividadeRequestDTO dto)
        {
            academicoService.EditarAtividade(id, dto);
            return NoContent();
        }

        [HttpDelete("atividades/{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult DeletarAtividade(int id)
        {
            academicoService.DeletarAtividade(id);
            return NoContent();
        }

        [HttpPost("atividade/entregar")]
        public async Task<IActionResult> EntregarAtividade([FromForm] EntregaAtividadeDTO dto)
        {
            if (dto.Arquivo == null || dto.Arquivo.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            return Ok("Atividade entregue com sucesso!");
        }
    }
}
