using edu_connect_backend.Context;
using edu_connect_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class ExtracurricularRepository
    {
        private readonly ConnectionContext context;

        public ExtracurricularRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public List<TurmaExtracurricular> ObterExtracurricularesPorAluno(int alunoId)
        {
            var aluno = this.context.alunos.FirstOrDefault(a => a.id == alunoId);
            if (aluno == null || aluno.turmaId == null) return new List<TurmaExtracurricular>();

            return this.context.TurmaExtracurriculares
                .Include(te => te.extracurricular)
                .Include(te => te.turma)
                .Include(te => te.professor).ThenInclude(p => p.usuario)
                .Where(te => te.turmaId == aluno.turmaId)
                .ToList();
        }

        public TurmaExtracurricular? ObterConteudoCompleto(int turmaExtracurricularId)
        {
            return this.context.TurmaExtracurriculares
                .Include(te => te.extracurricular)
                .Include(te => te.topicos)
                    .ThenInclude(t => t.materiais)
                .FirstOrDefault(te => te.id == turmaExtracurricularId);
        }

        public void CriarExtracurricular(Extracurricular atividade)
        {
            context.Extracurriculares.Add(atividade);
            context.SaveChanges();
        }

        public Extracurricular? ObterExtracurricularPorId(int id)
        {
            return context.Extracurriculares.FirstOrDefault(e => e.id == id);
        }

        public void AtualizarExtracurricular(Extracurricular atividade)
        {
            context.Extracurriculares.Update(atividade);
            context.SaveChanges();
        }

        public void DeletarExtracurricular(Extracurricular atividade)
        {
            context.Extracurriculares.Remove(atividade);
            context.SaveChanges();
        }

        public void VincularTurmaExtracurricular(TurmaExtracurricular vinculo)
        {
            context.TurmaExtracurriculares.Add(vinculo);
            context.SaveChanges();
        }

        public TurmaExtracurricular? ObterVinculoPorId(int id)
        {
            return context.TurmaExtracurriculares.FirstOrDefault(t => t.id == id);
        }

        public void DeletarVinculoExtracurricular(TurmaExtracurricular vinculo)
        {
            context.TurmaExtracurriculares.Remove(vinculo);
            context.SaveChanges();
        }
    }
}