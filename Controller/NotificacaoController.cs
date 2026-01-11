using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/notificacoes")]
    [Authorize] // Exige token JWT para acessar
    public class NotificacaoController : ControllerBase
    {
        private readonly NotificacaoService service;

        public NotificacaoController(NotificacaoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetNotificacoes()
        {
            try
            {
                var resultado = service.ObterNotificacoesDoUsuario();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("nao-lidas/contagem")]
        public IActionResult GetContagemNaoLidas()
        {
            try
            {
                var contagem = service.ContarNaoLidas();
                return Ok(contagem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/ler")]
        public IActionResult MarcarComoLida(int id)
        {
            var sucesso = service.MarcarComoLida(id);
            if (!sucesso) return NotFound("Notificação não encontrada.");

            return NoContent();
        }

        [HttpPut("ler-todas")]
        public IActionResult MarcarTodasComoLidas()
        {
            try
            {
                service.MarcarTodasComoLidas();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}