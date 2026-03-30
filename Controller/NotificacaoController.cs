using edu_connect_backend.Service;
using edu_connect_backend.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
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
        public IActionResult ListarNotificacoes()
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
        public IActionResult ObterContagemNotificacoesNaoLidas()
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
        public IActionResult MarcarNotificacaoComoLida(int id)
        {
            var sucesso = notificacaoService.MarcarNotificacaoComoLida(id, ColetaInfoToken.ObterIdUsuarioLogado(HttpContext));
            if (!sucesso) return NotFound("Notificação não encontrada.");

            return NoContent();
        }

        [HttpPut("ler-todas")]
        public IActionResult MarcarTodasNotificacoesComoLidas()
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