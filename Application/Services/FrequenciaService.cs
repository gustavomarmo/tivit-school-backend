using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Domain.Entities;
using edu_connect_backend.Infrastructure.Persistence.Context;
using edu_connect_backend.Infrastructure.Persistence.Repositories;

namespace edu_connect_backend.Application.Services
{
    public class FrequenciaService
    {
        private readonly FrequenciaRepository repository;
        private readonly AlunoRepository alunoRepository;
        private readonly ConnectionContext context;
        public FrequenciaService(
            FrequenciaRepository repository,
            AlunoRepository alunoRepository,
            ConnectionContext context)
        {
            this.repository = repository;
            this.alunoRepository = alunoRepository;
            this.context = context;
        }

        public void RealizarChamada(List<Frequencia> frequencias)
        {
            var idsAlunos = frequencias.Select(r => r.alunoId).Distinct().ToList();

            if (!alunoRepository.TodosAlunosExistem(idsAlunos))
            {
                throw new Exception("Um ou mais alunos informados não existem no banco de dados. Verifique a lista.");
            }

            repository.Registrar(frequencias);
        }

        public List<FrequenciaResumoReadModel> ObterResumoFrequencia(int usuarioId)
        {
            var aluno = alunoRepository.ObterAlunoPorUsuarioId(usuarioId)
                ?? throw new KeyNotFoundException("Aluno não encontrado.");

            return repository.ObterResumoPorAluno(aluno.id, aluno.turmaId)
                ?? throw new KeyNotFoundException("Resumo não encontrado.");
        }
    }
}