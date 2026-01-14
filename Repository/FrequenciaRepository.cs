using edu_connect_backend.Context;
using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class FrequenciaRepository
    {
        private readonly ConnectionContext context;

        public FrequenciaRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public void Registrar(List<Frequencia> frequencias)
        {
            context.Frequencias.AddRange(frequencias);
            context.SaveChanges();
        }

        public decimal ObterPercentualFrequenciaProfessor(int professorId)
        {
            var disciplinasDoProfessor = context.TurmaDisciplinas
                .Where(td => td.professorId == professorId)
                .Select(td => td.disciplinaId)
                .ToList();

            if (!disciplinasDoProfessor.Any()) return 0;

            var registros = context.Frequencias
                .Where(f => disciplinasDoProfessor.Contains(f.disciplinaId))
                .ToList();

            if (!registros.Any()) return 0;

            int total = registros.Count;
            int presentes = registros.Count(r => r.presente);

            return total == 0 ? 0 : Math.Round(((decimal)presentes / total) * 100, 1);
        }

        public List<FrequenciaResumoDTO> ObterResumoPorAluno(int alunoId, int? turmaId)
        {
            var consulta = context.TurmaDisciplinas
                .Where(td => td.turmaId == turmaId)
                .Select(td => new
                {
                    NomeDisciplina = td.disciplina.nome,
                    TotalAulas = context.Frequencias.Count(f => f.disciplinaId == td.disciplinaId && f.alunoId == alunoId),
                    Presencas = context.Frequencias.Count(f => f.disciplinaId == td.disciplinaId && f.alunoId == alunoId && f.presente)
                })
                .ToList();

            var resultado = consulta.Select(item => new FrequenciaResumoDTO
            {
                Disciplina = item.NomeDisciplina,
                TotalAulas = item.TotalAulas,
                Faltas = item.TotalAulas - item.Presencas,
                Frequencia = item.TotalAulas == 0
                    ? 100
                    : Math.Round(((decimal)item.Presencas / item.TotalAulas) * 100, 1)
            }).ToList();

            return resultado;
        }
    }
}