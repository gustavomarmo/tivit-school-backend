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
        private readonly AcademicoService academicoService;
        
        public MaterialController(AcademicoService academicoService)
        {
            this.academicoService = academicoService;
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Coordenador")]
        public IActionResult CriarMaterial([FromBody] MaterialRequestDTO dto)
        {
            academicoService.CriarMaterial(dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult EditarMaterial(int id, [FromBody] MaterialRequestDTO dto)
        {
            academicoService.EditarMaterial(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor,Admin")]
        public IActionResult DeletarMaterial(int id)
        {
            academicoService.DeletarMaterial(id);
            return NoContent();
        }
    }
}
