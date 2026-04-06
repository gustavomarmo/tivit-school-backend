using edu_connect_backend.DTO.Disciplina;
using edu_connect_backend.Mapper;
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
        private readonly TopicoService topicoService;
        private readonly TopicoMapper topicoMapper;

        public TopicoController(TopicoService topicoService, TopicoMapper topicoMapper)
        {
            this.topicoService = topicoService;
            this.topicoMapper = topicoMapper;
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult CriarTopico([FromBody] TopicoRequestDTO dto)
        {
            var topico = topicoMapper.ToTopico(dto);
            topicoService.CriarTopico(topico);

            return Created("", new { message = "Tópico criado com sucesso!" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult EditarTopico(int id, [FromBody] TopicoRequestDTO dto)
        {
            var topico = topicoMapper.ToTopico(dto);
            topicoService.EditarTopico(id, topico);

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