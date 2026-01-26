using edu_connect_backend.DTO;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("professor")]
    [Authorize]
    public class ProfessorController : ControllerBase
    {
        private readonly ProfessorService professorService;

        public ProfessorController(ProfessorService service)
        {
            this.professorService = service;
        }

        [HttpGet]
        public IActionResult listarProfessores([FromQuery] string? busca)
        {
            return Ok(professorService.listarProfessores(busca));
        }

        [HttpPost]
        public IActionResult criarProfessor([FromBody] ProfessorRequestDTO dto)
        {
            try
            {
                professorService.criarProfessor(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar professor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult editarProfessor(int id, [FromBody] ProfessorRequestDTO dto)
        {
            var resultado = professorService.editarProfessor(id, dto);

            if (resultado == null)
                return NotFound("Professor não encontrado.");

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult deletarProfessor(int id)
        {
            var sucesso = professorService.deletarProfessor(id);

            if (!sucesso)
                return NotFound("Professor não encontrado.");

            return NoContent();
        }
    }
}