using edu_connect_backend.Context;
using edu_connect_backend.DTO;
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
            var vinculos = context.TurmaDisciplinas.Where(td => td.turmaId == turma.id).ToList();
            context.TurmaDisciplinas.RemoveRange(vinculos);
            context.turmas.Remove(turma);
            context.SaveChanges();
        }

        public List<VinculoTurmaResponseDTO> ListarVinculos(int turmaId)
        {
            return context.TurmaDisciplinas
                .Include(td => td.disciplina)
                .Include(td => td.professor).ThenInclude(p => p.usuario)
                .Where(td => td.turmaId == turmaId)
                .Select(td => new VinculoTurmaResponseDTO
                {
                    id = td.id,
                    disciplina = td.disciplina.nome,
                    disciplinaId = td.disciplinaId,
                    professor = td.professor.usuario.nome,
                    professorId = td.professorId
                })
                .ToList();
        }

        public void RemoverVinculo(int vinculoId)
        {
            var vinculo = context.TurmaDisciplinas.FirstOrDefault(td => td.id == vinculoId)
                ?? throw new KeyNotFoundException("Vínculo não encontrado.");
            context.TurmaDisciplinas.Remove(vinculo);
            context.SaveChanges();
        }
    }
}