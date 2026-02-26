using edu_connect_backend.Context;
using edu_connect_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class NotaRepository
    {
        private readonly ConnectionContext context;

        public NotaRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public List<BoletimReadModel> ObterBoletimPorAluno(int alunoId)
        {
            return context.Database
                .SqlQueryRaw<BoletimReadModel>("EXEC sp_Notas_Boletim @AlunoId = {0}", alunoId)
                .ToList();
        }

        public List<NotaLancamentoReadModel> ObterAlunosParaLancamento(int turmaId, int disciplinaId, int bimestre)
        {
            return context.Database
                .SqlQueryRaw<NotaLancamentoReadModel>(
                    "EXEC sp_Notas_Lancamento @TurmaId = {0}, @DisciplinaId = {1}, @Bimestre = {2}",
                    turmaId, disciplinaId, bimestre)
                .ToList();
        }

        public Nota? ObterNotaEspecifica(int alunoId, int turmaDisciplinaId, int bimestre, string tipo)
        {
            return context.Notas
                .FirstOrDefault(n => n.alunoId == alunoId &&
                                     n.turmaDisciplinaId == turmaDisciplinaId &&
                                     n.bimestre == bimestre &&
                                     n.tipo == tipo);
        }

        public void Salvar(Nota nota)
        {
            context.Notas.Add(nota);
            context.SaveChanges();
        }

        public void Atualizar(Nota nota)
        {
            context.Notas.Update(nota);
            context.SaveChanges();
        }

        public TurmaDisciplina? ObterTurmaDisciplina(int turmaId, int disciplinaId)
        {
            return context.TurmaDisciplinas
                .FirstOrDefault(td => td.turmaId == turmaId && td.disciplinaId == disciplinaId);
        }
    }
}