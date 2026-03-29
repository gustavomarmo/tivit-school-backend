using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class TurmaService
    {
        private readonly TurmaRepository turma;

        public TurmaService(TurmaRepository turmaRepository)
        {
            this.turmaRepository = turmaRepository;
        }

        public List<Turma> ListarTurmas()
        {
            return turmaRepository.ListarTurmas()
                ?? throw new KeyNotFoundException("Turmas não encontradas");
        }

        public void CriarTurma(string nome, int anoLetivo)
        {
            var turma = new Turma
            {
                nome = nome,
                anoLetivo = anoLetivo
            };
            turmaRepository.Criar(turma);
        }

        public void DeletarTurma(int id)
        {
            var turma = turmaRepository.ObterPorId(id)
                ?? throw new KeyNotFoundException("Turma não encontrada.");
            turmaRepository.Deletar(turma);
        }

        public List<VinculoTurmaResponseDTO> ListarVinculos(int turmaId)
        {
            return turmaRepository.ListarVinculos(turmaId);
        }

        public void RemoverVinculo(int vinculoId)
        {
            turmaRepository.RemoverVinculo(vinculoId);
        }
    }
}