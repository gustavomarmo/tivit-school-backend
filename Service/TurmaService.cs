using edu_connect_backend.DTO.Turma;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class TurmaService
    {
        private readonly TurmaRepository turmaRepository;

        public TurmaService(TurmaRepository turmaRepository)
        {
            this.turmaRepository = turmaRepository;
        }

        public List<Turma> ListarTurmas()
        {
            return turmaRepository.ListarTurmas()
                ?? throw new KeyNotFoundException("Turmas não encontradas.");
        }

        public void CriarTurma(string nome, int anoLetivo)
        {
            var turma = new Turma { nome = nome, anoLetivo = anoLetivo };
            turmaRepository.Criar(turma);
        }

        public void DeletarTurma(int id)
        {
            var turma = turmaRepository.ObterPorId(id)
                ?? throw new KeyNotFoundException("Turma não encontrada.");

            var vinculos = turmaRepository.ObterVinculos(id);
            if (vinculos.Any())
                turmaRepository.RemoverVinculos(vinculos);

            turmaRepository.Deletar(turma);
        }

        public List<VinculoTurmaResponseDTO> ListarVinculos(int turmaId)
        {
            return turmaRepository.ObterVinculos(turmaId)
                .Select(td => new VinculoTurmaResponseDTO
                {
                    Id = td.id,
                    DisciplinaId = td.disciplinaId,
                    Disciplina = td.disciplina.nome,
                    ProfessorId = td.professorId,
                    Professor = td.professor?.usuario?.nome ?? "Sem Professor"
                })
                .ToList();
        }

        public void RemoverVinculo(int vinculoId)
        {
            var vinculo = turmaRepository.ObterVinculoPorId(vinculoId)
                ?? throw new KeyNotFoundException("Vínculo não encontrado.");

            turmaRepository.RemoverVinculos(new List<TurmaDisciplina> { vinculo });
        }
    }
}