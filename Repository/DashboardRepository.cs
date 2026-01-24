using edu_connect_backend.Context;
using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class DashboardRepository
    {
        private readonly ConnectionContext context;

        public DashboardRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public List<NotaResumoReadModel> ObterNotasRecentes(int alunoId)
        {
            return context.Database
                .SqlQueryRaw<NotaResumoReadModel>("EXEC sp_Dashboard_NotasRecentes @AlunoId = {0}", alunoId)
                .ToList();
        }

        public List<AvisoResumoReadModel> ObterAvisos(int turmaId)
        {
            return context.Database
                .SqlQueryRaw<AvisoResumoReadModel>("EXEC sp_Dashboard_Avisos @TurmaId = {0}", turmaId)
                .ToList();
        }

        public List<TarefaPendenteReadModel> ObterTarefasPendentes(int turmaId)
        {
            return context.Database
                .SqlQueryRaw<TarefaPendenteReadModel>("EXEC sp_Dashboard_Tarefas @TurmaId = {0}", turmaId)
                .ToList();
        }

        public List<DadosNotaProfessor> ObterNotasPorProfessor(int professorId)
        {
            var vinculos = context.TurmaDisciplinas
                .Where(td => td.professorId == professorId)
                .Include(td => td.turma)
                .Include(td => td.disciplina)
                .ToList();

            if (!vinculos.Any()) return new List<DadosNotaProfessor>();

            List<DadosNotaProfessor> resultados = new();

            foreach (var vinculo in vinculos)
            {
                var alunosNaTurma = context.alunos
                    .Include(a => a.usuario)
                    .Where(a => a.turmaId == vinculo.turmaId)
                    .ToList();

                foreach (var aluno in alunosNaTurma)
                {
                    
                    var notas = context.Notas
                        .Where(n => n.alunoId == aluno.id && n.turmaDisciplinaId == vinculo.id)
                        .ToList();

                    decimal mediaAtual = notas.Any() ? notas.Average(n => n.valor) : 0;

                    resultados.Add(new DadosNotaProfessor
                    {
                        AlunoId = aluno.id,
                        AlunoNome = aluno.usuario.nome,
                        TurmaNome = vinculo.turma.nome,
                        DisciplinaNome = vinculo.disciplina.nome,
                        Media = mediaAtual
                    });
                }
            }

            return resultados;
        }

        public KPIsProfessorDTO ObterKPIsProfessorProcedure(int professorId)
        {
            var param = new SqlParameter("@ProfessorId", professorId);

            var result = context.Database
                .SqlQueryRaw<KPIsProfessorDTO>("EXEC sp_Dashboard_Professor_KPIs @ProfessorId", param)
                .AsEnumerable()
                .FirstOrDefault();

            return result ?? new KPIsProfessorDTO();
        }

        public List<AlunoAtencaoDTO> ObterAlunosEmRisco(int professorId)
        {
            var param = new SqlParameter("@ProfessorId", professorId);

            return context.Database
                .SqlQueryRaw<AlunoAtencaoDTO>("EXEC sp_Dashboard_Professor_AlunosRisco @ProfessorId", param)
                .ToList();
        }

        public KPIsCoordenadorDTO ObterKPIsCoordenador()
        {
            var result = context.Database
                .SqlQueryRaw<KPIsCoordenadorDTO>("EXEC sp_Dashboard_Coordenador_KPIs")
                .AsEnumerable()
                .FirstOrDefault();
            return result ?? new KPIsCoordenadorDTO();
        }

        public List<GraficoBarrasDTO> ObterGraficoDesempenhoTurmas()
        {
            return context.Database
                .SqlQueryRaw<GraficoBarrasDTO>("EXEC sp_Dashboard_Coordenador_GraficoBarras")
                .ToList();
        }

        public List<GraficoPizzaDTO> ObterGraficoStatusAlunos()
        {
            return context.Database
                .SqlQueryRaw<GraficoPizzaDTO>("EXEC sp_Dashboard_Coordenador_GraficoPizza")
                .ToList();
        }
    }

    public class DadosNotaProfessor
    {
        public int AlunoId { get; set; }
        public string AlunoNome { get; set; }
        public string TurmaNome { get; set; }
        public string DisciplinaNome { get; set; }
        public decimal Media { get; set; }
    }
}