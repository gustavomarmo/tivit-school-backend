using edu_connect_backend.DTO;
using edu_connect_backend.Mapper;
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
        private readonly AlunoMapper mapper;

        public AlunoController(AlunoService service, AlunoMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult ListarAlunos([FromQuery] string? busca)
        {
            return Ok(mapper.ToAlunoResponseDTOList(service.ListarAlunos(busca)));
        }

        [HttpPost]
        public IActionResult CriarAluno([FromBody] AlunoRequestDTO dto)
        {
            service.CriarAluno(mapper.ToAluno(dto));
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult EditarAluno(int id, [FromBody] AlunoRequestDTO dto)
        {
            var sucesso = service.EditarAluno(id, mapper.ToAluno(dto));

            if (sucesso == null)
                return NotFound("Aluno não encontrado.");

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarAluno(int id)
        {
            var sucesso = service.DeletarAluno(id);

            if (!service.DeletarAluno(id))
                return NotFound("Aluno não encontrado.");

            return NoContent();
        }
    }
}