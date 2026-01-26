using edu_connect_backend.Service;
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
        public IActionResult listarNotificacoes()
        {
            try
            {
                var resultado = notificacaoService.obterNotificacoesDoUsuario();
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
                var contagem = notificacaoService.contarNotificacoesNaoLidas();
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
            var sucesso = notificacaoService.marcarNotificacaoComoLida(id);
            if (!sucesso) return NotFound("Notificação não encontrada.");

            return NoContent();
        }

        [HttpPut("ler-todas")]
        public IActionResult marcarTodasNotificacoesComoLidas()
        {
            try
            {
                notificacaoService.marcarTodasNotificacoesComoLidas();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}