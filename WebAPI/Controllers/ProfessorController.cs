using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Application.Mappers;
using edu_connect_backend.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/professores")]
    [Authorize]
    public class ProfessorController : ControllerBase
    {
        private readonly ProfessorService professorService;
        private readonly ProfessorMapper professorMapper;

        public ProfessorController(ProfessorService professorService, ProfessorMapper professorMapper)
        {
            this.professorService = professorService;
            this.professorMapper = professorMapper;
        }

        [HttpGet]
        public IActionResult ListarProfessores([FromQuery] string? busca)
        {
            var professoresModel = professorService.ListarProfessores(busca);
            var professoresDTO = professorMapper.ToProfessorResponseDTOList(professoresModel);

            return Ok(professoresDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult CriarProfessor([FromBody] ProfessorRequestDTO dto)
        {
            try
            {
                var professorModel = professorMapper.ToProfessor(dto);
                professorService.CriarProfessor(professorModel);

                return Created("", new { message = "Professor criado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao criar professor: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult EditarProfessor(int id, [FromBody] ProfessorRequestDTO dto)
        {
            var professorModel = professorMapper.ToProfessor(dto);
            var resultado = professorService.EditarProfessor(id, professorModel);

            if (resultado == null)
                return NotFound(new { message = "Professor não encontrado." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult DeletarProfessor(int id)
        {
            var sucesso = professorService.DeletarProfessor(id);

            if (!sucesso)
                return NotFound(new { message = "Professor não encontrado." });

            return NoContent();
        }
    }
}