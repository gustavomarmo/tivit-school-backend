using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Application.Mappers;
using edu_connect_backend.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/turmas")]
    [Authorize]
    public class TurmaController : ControllerBase
    {
        private readonly TurmaService turmaService;
        private readonly TurmaMapper turmaMapper;

        public TurmaController(TurmaService turmaService, TurmaMapper turmaMapper)
        {
            this.turmaService = turmaService;
            this.turmaMapper = turmaMapper;
        }

        [HttpGet]
        [Authorize(Roles = "Professor,Coordenador,Admin")]
        public IActionResult ListarTurmas()
        {
            var turmas = turmaService.ListarTurmas();
            return Ok(turmaMapper.ToTurmaResponseDTOList(turmas));
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult CriarTurma([FromBody] TurmaRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest(new { message = "O nome da turma é obrigatório." });

            turmaService.CriarTurma(dto.Nome, dto.AnoLetivo);
            return Created("", new { message = "Turma criada com sucesso!" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult DeletarTurma(int id)
        {
            turmaService.DeletarTurma(id);
            return NoContent();
        }

        [HttpGet("{id}/vinculos")]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult ListarVinculos(int id)
        {
            var vinculos = turmaService.ListarVinculos(id);
            return Ok(vinculos);
        }

        [HttpDelete("vinculos/{vinculoId}")]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult RemoverVinculo(int vinculoId)
        {
            turmaService.RemoverVinculo(vinculoId);
            return NoContent();
        }
    }
}