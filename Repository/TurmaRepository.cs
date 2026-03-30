using edu_connect_backend.Context;
using edu_connect_backend.Model;
using Microsoft.EntityFrameworkCore;

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
            return context.turmas.OrderBy(t => t.nome).ToList();
        }

        public Turma? ObterPorId(int id)
        {
            return context.turmas.FirstOrDefault(t => t.id == id);
        }

        public void Criar(Turma turma)
        {
            context.turmas.Add(turma);
            context.SaveChanges();
        }

        public void Deletar(Turma turma)
        {
            context.turmas.Remove(turma);
            context.SaveChanges();
        }

        public List<TurmaDisciplina> ObterVinculos(int turmaId)
        {
            return context.TurmaDisciplinas
                .Include(td => td.disciplina)
                .Include(td => td.professor).ThenInclude(p => p.usuario)
                .Where(td => td.turmaId == turmaId)
                .ToList();
        }

        public void RemoverVinculos(List<TurmaDisciplina> vinculos)
        {
            context.TurmaDisciplinas.RemoveRange(vinculos);
            context.SaveChanges();
        }

        public TurmaDisciplina? ObterVinculoPorId(int vinculoId)
        {
            return context.TurmaDisciplinas.FirstOrDefault(td => td.id == vinculoId);
        }
    }
}