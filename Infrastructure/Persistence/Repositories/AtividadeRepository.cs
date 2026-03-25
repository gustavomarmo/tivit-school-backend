using edu_connect_backend.Domain.Entities;
using edu_connect_backend.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Infrastructure.Persistence.Repositories
{
    public class AtividadeRepository
    {
        private readonly ConnectionContext context;

        public AtividadeRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public void AdicionarEntrega(Entrega entrega)
        {
            this.context.Entregas.Add(entrega);
            this.context.SaveChanges();
        }

        public bool ExisteEntrega(int materialId, int alunoId)
        {
            return this.context.Entregas.Any(e => e.materialId == materialId && e.alunoId == alunoId);
        }

        public List<int> ObterIdsMateriaisEntregues(int turmaDisciplinaId, int alunoId)
        {
            return context.Entregas
                .Include(e => e.material)
                .Where(e => e.alunoId == alunoId && e.material.topico.turmaDisciplinaId == turmaDisciplinaId)
                .Select(e => e.materialId)
                .ToList();
        }
    }
}