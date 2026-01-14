using edu_connect_backend.Context;
using edu_connect_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class EventoRepository
    {
        private readonly ConnectionContext context;

        public EventoRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public void Criar(Evento evento)
        {
            context.Eventos.Add(evento);
            context.SaveChanges();
        }

        public List<Evento> ObterPorMesAno(int mes, int ano)
        {
            return context.Eventos
                .Include(e => e.turma)
                .Where(e => e.dataInicio.Month == mes && e.dataInicio.Year == ano)
                .OrderBy(e => e.dataInicio)
                .ToList();
        }
    }
}