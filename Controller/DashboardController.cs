using edu_connect_backend.Mapper;
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
        private readonly DashboardService dashboardService;
        private readonly DashboardMapper dashboardMapper;

        public DashboardController(DashboardService service)
        {
            this.dashboardService = service;
        }

        [HttpGet("aluno")]
        [Authorize(Roles = "Aluno")]
        public IActionResult GetDashboardAluno()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email == null) return Unauthorized();

            var resultado = dashboardService.ObterDashboardAluno(email);

            return Ok(resultado);
        }

        [HttpGet("professor")]
        [Authorize(Roles = "Professor")]
        public IActionResult GetDashboardProfessor()
        {
            try
            {
                var dados = dashboardService.ObterDashboardProfessor();
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
                var dados = dashboardService.ObterDashboardCoordenador();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}