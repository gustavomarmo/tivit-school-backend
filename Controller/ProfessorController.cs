using edu_connect_backend.DTO.Professor;
using edu_connect_backend.Mapper;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
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
            return Ok(professorMapper.ToProfessorResponseDTOList(professoresModel));
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult CriarProfessor([FromBody] ProfessorRequestDTO dto)
        {
            professorService.CriarProfessor(professorMapper.ToProfessor(dto));
            return Created("", new { message = "Professor criado com sucesso!" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult EditarProfessor(int id, [FromBody] ProfessorRequestDTO dto)
        {
            var professorModel = professorMapper.ToProfessor(dto);
            professorService.EditarProfessor(id, professorModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult DeletarProfessor(int id)
        {
            professorService.DeletarProfessor(id);
            return NoContent();
        }
    }
}