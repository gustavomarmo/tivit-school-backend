using edu_connect_backend.Application.Services;
using edu_connect_backend.WebAPI.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace edu_connect_backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/dashboards")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet("aluno")]
        [Authorize(Roles = "Aluno")]
        public IActionResult ObterDashboardAluno()
        {
            var usuarioId = ColetaInfoToken.ObterIdUsuarioLogado(HttpContext);

            if (usuarioId == null) return Unauthorized();

            return Ok(dashboardService.obterDashboardAluno(usuarioId));
        }

        [HttpGet("professor")]
        [Authorize(Roles = "Professor")]
        public IActionResult ObterDashboardProfessor()
        {
            var usuarioId = ColetaInfoToken.ObterIdUsuarioLogado(HttpContext);

            if (usuarioId == null) return Unauthorized();

            return Ok(dashboardService.ObterDashboardProfessor(usuarioId));
        }

        [HttpGet("coordenador")]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult ObterDashboardCoordenador()
        {
            return Ok(dashboardService.ObterDashboardCoordenador());
        }
    }
}