using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Infrastructure.AI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/exercicios")]
    [Authorize]
    public class ExerciciosController : ControllerBase
    {
        private readonly AiService aiService;

        public ExerciciosController(AiService aiService)
        {
            this.aiService = aiService;
        }

        [HttpPost("gerar")]
        public async Task<IActionResult> GerarExercicios([FromBody] GerarExerciciosRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TextoConteudo))
                return BadRequest(new { mensagem = "O conteúdo do material é obrigatório." });

            var textoCortado = dto.TextoConteudo.Length > 8000
                ? dto.TextoConteudo[..8000]
                : dto.TextoConteudo;

            var conteudoGerado = await aiService.GerarExercicios(textoCortado, dto.NomeMaterial);

            return Ok(new GerarExerciciosResponseDTO { Conteudo = conteudoGerado });
        }
    }
}