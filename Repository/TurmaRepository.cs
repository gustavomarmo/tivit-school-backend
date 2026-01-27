using edu_connect_backend.Context;
using edu_connect_backend.Model;

namespace edu_connect_backend.Repository
{
    public class TurmaRepository
    {
        private readonly ConnectionContext context;

        public TurmaRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public List<Turma> ListarTurmas()
        {
            return this.context.turmas.OrderBy(t => t.nome).ToList();
        }
    }
}