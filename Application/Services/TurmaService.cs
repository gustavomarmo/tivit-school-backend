using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Domain.Entities;
using edu_connect_backend.Infrastructure.Persistence.Repositories;

namespace edu_connect_backend.Application.Services
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