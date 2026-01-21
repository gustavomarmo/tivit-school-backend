using edu_connect_backend.Context;
using edu_connect_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class AcademicoRepository
    {
        private readonly ConnectionContext context;

        public AcademicoRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public List<Turma> ListarTodasTurmas()
        {
            return context.turmas.OrderBy(t => t.nome).ToList();
        }

        public List<Disciplina> ListarDisciplinasGerais()
        {
            return context.Disciplinas.ToList();
        }

        public void CriarDisciplina(Disciplina disciplina)
        {
            context.Disciplinas.Add(disciplina);
            context.SaveChanges();
        }

        public void VincularDisciplinaTurma(TurmaDisciplina vinculo)
        {
            context.TurmaDisciplinas.Add(vinculo);
            context.SaveChanges();
        }

        public List<TurmaDisciplina> ObterDisciplinasPorAluno(int alunoId)
        {
            var aluno = context.alunos.FirstOrDefault(a => a.id == alunoId);
            if (aluno == null || aluno.turmaId == null) return new List<TurmaDisciplina>();

            return context.TurmaDisciplinas
                .Include(td => td.disciplina)
                .Include(td => td.professor).ThenInclude(p => p.usuario)
                .Include(td => td.turma)
                .Where(td => td.turmaId == aluno.turmaId)
                .ToList();
        }

        public List<TurmaDisciplina> ObterDisciplinasPorProfessor(int professorId)
        {
            return context.TurmaDisciplinas
                .Include(td => td.disciplina)
                .Include(td => td.turma)
                .Where(td => td.professorId == professorId)
                .ToList();
        }

        public TurmaDisciplina? ObterConteudoCompleto(int turmaDisciplinaId)
        {
            return context.TurmaDisciplinas
                .Include(td => td.disciplina)
                .Include(td => td.topicos)
                .ThenInclude(t => t.materiais)
                .FirstOrDefault(td => td.id == turmaDisciplinaId);
        }

        public void AdicionarTopico(Topico topico)
        {
            context.Topicos.Add(topico);
            context.SaveChanges();
        }

        public void AdicionarMaterial(Material material)
        {
            context.Materiais.Add(material);
            context.SaveChanges();
        }

        public Material? ObterMaterialPorId(int id)
        {
            return context.Materiais.FirstOrDefault(m => m.id == id);
        }

        public void AtualizarMaterial(Material material)
        {
            context.Materiais.Update(material);
            context.SaveChanges();
        }

        public void DeletarMaterial(Material material)
        {
            context.Materiais.Remove(material);
            context.SaveChanges();
        }

        public void AdicionarEntrega(Entrega entrega)
        {
            context.Entregas.Add(entrega);
            context.SaveChanges();
        }

        public bool ExisteEntrega(int materialId, int alunoId)
        {
            return context.Entregas.Any(e => e.materialId == materialId && e.alunoId == alunoId);
        }

        public List<TurmaExtracurricular> ObterExtracurricularesPorAluno(int alunoId)
        {
            var aluno = context.alunos.FirstOrDefault(a => a.id == alunoId);
            if (aluno == null || aluno.turmaId == null) return new List<TurmaExtracurricular>();

            return context.TurmaExtracurriculares
                .Include(te => te.extracurricular)
                .Include(te => te.turma)
                .Include(te => te.professor).ThenInclude(p => p.usuario)
                .Where(te => te.turmaId == aluno.turmaId)
                .ToList();
        }

        public TurmaExtracurricular? ObterConteudoExtracurricularCompleto(int turmaExtracurricularId)
        {
            return context.TurmaExtracurriculares
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