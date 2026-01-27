using edu_connect_backend.DTO;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/topicos")]
    [Authorize]
    public class TopicoController : ControllerBase
    {
        private readonly AcademicoService academicoService;

        public TopicoController(AcademicoService academicoService)
        {
            this.academicoService = academicoService;
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult CriarTopico([FromBody] TopicoRequestDTO dto)
        {
            academicoService.CriarTopico(dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult EditarTopico(int id, [FromBody] TopicoRequestDTO dto)
        {
            academicoService.EditarTopico(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult DeletarTopico(int id)
        {
            academicoService.DeletarTopico(id);
            return NoContent();
        }
    }
}
