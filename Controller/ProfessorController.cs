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
        private readonly ProfessorService service;

        public ProfessorController(ProfessorService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Listar([FromQuery] string? busca)
        {
            return Ok(service.ListarProfessores(busca));
        }

        [HttpPost]
        public IActionResult Criar([FromBody] ProfessorRequestDTO dto)
        {
            try
            {
                service.CriarProfessor(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar professor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] ProfessorRequestDTO dto)
        {
            var resultado = service.EditarProfessor(id, dto);

            if (resultado == null)
                return NotFound("Professor não encontrado.");

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var sucesso = service.DeletarProfessor(id);

            if (!sucesso)
                return NotFound("Professor não encontrado.");

            return NoContent();
        }
    }
}