using edu_connect_backend.Application.Services;
using edu_connect_backend.WebAPI.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/notificacoes")]
    [Authorize]
    public class NotificacaoController : ControllerBase
    {
        private readonly NotificacaoService notificacaoService;

        public NotificacaoController(NotificacaoService service)
        {
            this.notificacaoService = service;
        }

        [HttpGet]
        public IActionResult listarNotificacoes()
        {
            try
            {
                var resultado = notificacaoService.ObterNotificacoesDoUsuario(ColetaInfoToken.ObterIdUsuarioLogado(HttpContext));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("nao-lidas/contagem")]
        public IActionResult obterContagemNotificacoesNaoLidas()
        {
            try
            {
                var contagem = notificacaoService.ContarNotificacoesNaoLidas(ColetaInfoToken.ObterIdUsuarioLogado(HttpContext));
                return Ok(contagem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/ler")]
        public IActionResult marcarNotificacaoComoLida(int id)
        {
            var sucesso = notificacaoService.MarcarNotificacaoComoLida(id, ColetaInfoToken.ObterIdUsuarioLogado(HttpContext));
            if (!sucesso) return NotFound("Notificação não encontrada.");

            return NoContent();
        }

        [HttpPut("ler-todas")]
        public IActionResult marcarTodasNotificacoesComoLidas()
        {
            try
            {
                notificacaoService.MarcarTodasNotificacoesComoLidas(ColetaInfoToken.ObterIdUsuarioLogado(HttpContext));
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}