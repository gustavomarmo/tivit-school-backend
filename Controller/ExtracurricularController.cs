using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/extracurriculares")]
    [Authorize]
    public class ExtracurricularController : ControllerBase
    {
        private readonly AcademicoService service;

        public ExtracurricularController(AcademicoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetExtracurriculares()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            try
            {
                var resultado = service.ListarExtracurriculares(email);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao listar atividades: " + ex.Message);
            }
        }

        [HttpGet("{id}/conteudo")]
        public IActionResult GetConteudo(int id)
        {
            try
            {
                var conteudo = service.ObterConteudoExtracurricular(id);
                if (conteudo == null) return NotFound("Atividade não encontrada.");
                return Ok(conteudo);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao obter conteúdo: " + ex.Message);
            }
        }
    }
}