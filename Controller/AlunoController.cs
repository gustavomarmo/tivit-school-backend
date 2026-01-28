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
        private readonly AlunoService alunoService;
        private readonly AlunoMapper alunoMapper;

        public AlunoController(AlunoService alunoService, AlunoMapper alunoMapper)
        {
            this.alunoService = alunoService;
            this.alunoMapper = alunoMapper;
        }

        [HttpGet]
        public IActionResult ListarAlunos([FromQuery] string? busca)
        {
            return Ok(alunoMapper.ToAlunoResponseDTOList(alunoService.ListarAlunos(busca)));
        }

        [HttpPost]
        public IActionResult CriarAluno([FromBody] AlunoRequestDTO dto)
        {
            alunoService.CriarAluno(alunoMapper.ToAluno(dto));
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult EditarAluno(int id, [FromBody] AlunoRequestDTO dto)
        {
            var sucesso = alunoService.EditarAluno(id, alunoMapper.ToAluno(dto));

            if (sucesso == null)
                return NotFound("Aluno não encontrado.");

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarAluno(int id)
        {
            var sucesso = alunoService.DeletarAluno(id);

            if (!alunoService.DeletarAluno(id))
                return NotFound("Aluno não encontrado.");

            return NoContent();
        }
    }
}