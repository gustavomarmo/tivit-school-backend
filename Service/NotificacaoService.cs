using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using System.Security.Claims;

namespace edu_connect_backend.Service
{
    public class NotificacaoService
    {
        private readonly NotificacaoRepository repository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public NotificacaoService(NotificacaoRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            this.httpContextAccessor = httpContextAccessor;
        }

        private int ObterIdUsuarioLogado()
        {
            var idClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id))
            {
                return id;
            }
            throw new Exception("Usuário não identificado no token.");
        }

        public List<NotificacaoResponseDTO> ObterNotificacoesDoUsuario()
        {
            var usuarioId = ObterIdUsuarioLogado();
            var notificacoes = repository.ObterPorUsuario(usuarioId);

            return notificacoes.Select(n => new NotificacaoResponseDTO
            {
                id = n.id,
                title = n.titulo,
                message = n.mensagem,
                type = n.tipo,
                read = n.lida,
                timestamp = new DateTimeOffset(n.dataCriacao).ToUnixTimeMilliseconds(),
                time = CalcularTempoRelativo(n.dataCriacao)
            }).ToList();
        }

        public int ContarNaoLidas()
        {
            var usuarioId = ObterIdUsuarioLogado();
            return repository.ContarNaoLidas(usuarioId);
        }

        public bool MarcarComoLida(int id)
        {
            var usuarioId = ObterIdUsuarioLogado();
            var notificacao = repository.ObterPorId(id, usuarioId);

            if (notificacao == null) return false;

            repository.MarcarComoLida(notificacao);
            return true;
        }

        public void MarcarTodasComoLidas()
        {
            var usuarioId = ObterIdUsuarioLogado();
            repository.MarcarTodasComoLidas(usuarioId);
        }

        private string CalcularTempoRelativo(DateTime data)
        {
            var diff = DateTime.Now - data;
            if (diff.TotalMinutes < 1) return "Agora mesmo";
            if (diff.TotalMinutes < 60) return $"{Math.Floor(diff.TotalMinutes)} min atrás";
            if (diff.TotalHours < 24) return $"{Math.Floor(diff.TotalHours)} horas atrás";
            if (diff.TotalDays < 2) return "Ontem";
            return $"{Math.Floor(diff.TotalDays)} dias atrás";
        }
    }
}