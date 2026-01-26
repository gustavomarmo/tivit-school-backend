using edu_connect_backend.DTO;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("aluno")]
    [Authorize]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService service;

        public AlunoController(AlunoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult listarAlunos([FromQuery] string? busca)
        {
            var resultado = service.listarAlunos(busca);
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult criarAluno([FromBody] AlunoRequestDTO dto)
        {
            try
            {
                service.criarAluno(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult editarAluno(int id, [FromBody] AlunoRequestDTO dto)
        {
            var alunoEditado = service.editarAluno(id, dto);

            if (alunoEditado == null)
                return NotFound("Aluno não encontrado.");

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult deletarAluno(int id)
        {
            var sucesso = service.deletarAluno(id);

            if (!sucesso)
                return NotFound("Aluno não encontrado.");

            return NoContent();
        }
    }
}