using edu_connect_backend.Context;
using edu_connect_backend.Model;

namespace edu_connect_backend.Repository
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
    }
}