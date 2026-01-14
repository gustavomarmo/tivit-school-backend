using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/dashboard")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService service;

        public DashboardController(DashboardService service)
        {
            this.service = service;
        }

        [HttpGet("aluno")]
        [Authorize(Roles = "Aluno")]
        public IActionResult GetDashboardAluno()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var resultado = service.ObterDashboardAluno(email);

            if (resultado == null)
                return NotFound("Aluno não encontrado ou sem turma vinculada.");

            return Ok(resultado);
        }

        [HttpGet("professor")]
        [Authorize(Roles = "Professor")]
        public IActionResult GetDashboardProfessor()
        {
            try
            {
                var dados = service.ObterDashboardProfessor();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("coordenador")]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult GetDashboardCoordenador()
        {
            try
            {
                var dados = service.ObterDashboardCoordenador();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}