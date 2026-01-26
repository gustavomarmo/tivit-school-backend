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
        public void Atualizar(Evento evento)
        {
            context.Eventos.Update(evento);
            context.SaveChanges();
        }

        public void Deletar(Evento evento)
        {
            context.Eventos.Remove(evento);
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

        public List<Evento> obterProximosEventos(int quantidade)
        {
            return context.Eventos
                .Include(e => e.turma)
                .Where(e => e.dataInicio >= DateTime.Today)
                .OrderBy(e => e.dataInicio)
                .Take(quantidade)
                .ToList();
        }

        public Evento? ObterPorId(int id)
        {
            return context.Eventos.FirstOrDefault(e => e.id == id);
        }
    }
}