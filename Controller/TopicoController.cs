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
        private TopicoService topicoService;

        public TopicoController(TopicoService topicoService)
        {
            this.topicoService = topicoService;
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult CriarTopico([FromBody] TopicoRequestDTO dto)
        {
            topicoService.CriarTopico(dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult EditarTopico(int id, [FromBody] TopicoRequestDTO dto)
        {
            topicoService.EditarTopico(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult DeletarTopico(int id)
        {
            topicoService.DeletarTopico(id);
            return NoContent();
        }
    }
}
