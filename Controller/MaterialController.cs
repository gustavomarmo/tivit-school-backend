using edu_connect_backend.DTO;
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
        
        public MaterialController(MaterialService materialService)
        {
            this.materialService = materialService;
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult CriarMaterial([FromBody] MaterialRequestDTO dto)
        {
            materialService.CriarMaterial(dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult EditarMaterial(int id, [FromBody] MaterialRequestDTO dto)
        {
            materialService.EditarMaterial(id, dto);
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
