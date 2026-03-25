using edu_connect_backend.Domain.Entities;
using edu_connect_backend.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Infrastructure.Persistence.Repositories
{
    public class DisciplinaRepository
    {
        private readonly ConnectionContext context;

        public DisciplinaRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public void CriarDisciplina(Disciplina disciplina)
        {
            this.context.Disciplinas.Add(disciplina);
            this.context.SaveChanges();
        }

        public void VincularDisciplina(TurmaDisciplina vinculo)
        {
            this.context.TurmaDisciplinas.Add(vinculo);
            this.context.SaveChanges();
        }

        public List<TurmaDisciplina> ObterDisciplinasPorAlunoId(int alunoId)
        {
            var aluno = this.context.alunos.FirstOrDefault(a => a.id == alunoId);
            if (aluno == null || aluno.turmaId == null) return new List<TurmaDisciplina>();

            return this.context.TurmaDisciplinas
                .Include(td => td.disciplina)
                .Include(td => td.professor).ThenInclude(p => p.usuario)
                .Include(td => td.turma)
                .Where(td => td.turmaId == aluno.turmaId)
                .ToList();
        }

        public List<TurmaDisciplina> ObterDisciplinasPorProfessorId(int professorId)
        {
            return this.context.TurmaDisciplinas
                .Include(td => td.disciplina)
                .Include(td => td.turma)
                .Where(td => td.professorId == professorId)
                .ToList();
        }

        public TurmaDisciplina? ObterConteudoCompleto(int turmaDisciplinaId)
        {
            return this.context.TurmaDisciplinas
                .Include(td => td.disciplina)
                .Include(td => td.topicos)
                .ThenInclude(t => t.materiais)
                .FirstOrDefault(td => td.id == turmaDisciplinaId);
        }

        public List<Disciplina> ListarTodasDisciplinas()
        {
            return context.Disciplinas.OrderBy(d => d.nome).ToList();
        }
    }
}