using edu_connect_backend.DTO;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/aluno")]
    [Authorize]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService service;

        public AlunoController(AlunoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult ListarAlunos([FromQuery] string? busca)
        {
            var resultado = service.ListarAlunos(busca);
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult CriarAluno([FromBody] AlunoRequestDTO dto)
        {
            service.CriarAluno(dto);
            return StatusCode(201);    
        }

        [HttpPut("{id}")]
        public IActionResult EditarAluno(int id, [FromBody] AlunoRequestDTO dto)
        {
            var alunoEditado = service.EditarAluno(id, dto);

            if (alunoEditado == null)
                return NotFound("Aluno não encontrado.");

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarAluno(int id)
        {
            var sucesso = service.DeletarAluno(id);

            if (!sucesso)
                return NotFound("Aluno não encontrado.");

            return NoContent();
        }
    }
}