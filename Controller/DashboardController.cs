using edu_connect_backend.Service;
using edu_connect_backend.Util;
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

        public DashboardController(DashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet("aluno")]
        [Authorize(Roles = "Aluno")]
        public IActionResult getDashboardAluno()
        {
            var usuarioId = ColetaInfoToken.ObterIdUsuarioLogado(HttpContext);

            if (usuarioId == null) return Unauthorized();

            return Ok(dashboardService.obterDashboardAluno(usuarioId));
        }

        [HttpGet("professor")]
        [Authorize(Roles = "Professor")]
        public IActionResult getDashboardProfessor()
        {
            var usuarioId = ColetaInfoToken.ObterIdUsuarioLogado(HttpContext);

            if (usuarioId == null) return Unauthorized();

            return Ok(dashboardService.obterDashboardProfessor(usuarioId));
        }

        [HttpGet("coordenador")]
        [Authorize(Roles = "Coordenador,Admin")]
        public IActionResult getDashboardCoordenador()
        {
            try
            {
                var dados = dashboardService.obterDashboardCoordenador();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}