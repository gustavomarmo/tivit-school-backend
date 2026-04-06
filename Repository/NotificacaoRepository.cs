using edu_connect_backend.Context;
using edu_connect_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class NotificacaoRepository
    {
        private readonly ConnectionContext context;

        public NotificacaoRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public List<Notificacao> ObterPorUsuario(int usuarioId)
        {
            return context.Notificacoes
                .Where(n => n.usuarioId == usuarioId)
                .OrderByDescending(n => n.dataCriacao)
                .ToList();
        }

        public int ContarNaoLidas(int usuarioId)
        {
            return context.Notificacoes
                .Count(n => n.usuarioId == usuarioId && !n.lida);
        }

        public Notificacao? ObterPorId(int id, int usuarioId)
        {
            return context.Notificacoes
                .FirstOrDefault(n => n.id == id && n.usuarioId == usuarioId);
        }

        public void MarcarComoLida(Notificacao notificacao)
        {
            notificacao.lida = true;
            context.Notificacoes.Update(notificacao);
            context.SaveChanges();
        }

        public void MarcarTodasComoLidas(int usuarioId)
        {
            var notificacoes = context.Notificacoes
                .Where(n => n.usuarioId == usuarioId && !n.lida)
                .ToList();

            foreach (var notif in notificacoes)
                notif.lida = true;

            context.SaveChanges();
        }

        public void Criar(Notificacao notificacao)
        {
            context.Notificacoes.Add(notificacao);
            context.SaveChanges();
        }

        public void CriarParaAluno(int alunoId, string tipo, string titulo, string mensagem)
        {
            var usuarioId = context.alunos
                .Where(a => a.id == alunoId)
                .Select(a => a.usuarioId)
                .FirstOrDefault();

            if (usuarioId == 0) return;

            context.Notificacoes.Add(new Notificacao
            {
                usuarioId = usuarioId,
                tipo = tipo,
                titulo = titulo,
                mensagem = mensagem,
                dataCriacao = DateTime.Now,
                lida = false
            });

            context.SaveChanges();
        }

        public void CriarParaUsuario(int usuarioId, string tipo, string titulo, string mensagem)
        {
            context.Notificacoes.Add(new Notificacao
            {
                usuarioId = usuarioId,
                tipo = tipo,
                titulo = titulo,
                mensagem = mensagem,
                dataCriacao = DateTime.Now,
                lida = false
            });
            context.SaveChanges();
        }
    }
}