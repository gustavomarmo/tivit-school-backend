using edu_connect_backend.DTO.Disciplina;
using edu_connect_backend.Mapper;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/materiais")]
    [Authorize]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialService materialService;
        private readonly MaterialMapper materialMapper;

        public MaterialController(MaterialService materialService, MaterialMapper materialMapper)
        {
            this.materialService = materialService;
            this.materialMapper = materialMapper;
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult CriarMaterial([FromBody] MaterialRequestDTO dto)
        {
            var material = materialMapper.ToMaterial(dto);
            materialService.CriarMaterial(material);
            return Created("", new { message = "Material criado com sucesso!" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult EditarMaterial(int id, [FromBody] MaterialRequestDTO dto)
        {
            var material = materialMapper.ToMaterial(dto);
            materialService.EditarMaterial(id, material);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult DeletarMaterial(int id)
        {
            materialService.DeletarMaterial(id);
            return NoContent();
        }
    }
}