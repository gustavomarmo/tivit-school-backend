using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class FrequenciaService
    {
        private readonly FrequenciaRepository repository;
        private readonly AlunoRepository alunoRepository;

        public FrequenciaService(
            FrequenciaRepository repository,
            AlunoRepository alunoRepository)
        {
            this.repository = repository;
            this.alunoRepository = alunoRepository;
        }

        public void RealizarChamada(List<Frequencia> frequencias)
        {
            var idsAlunos = frequencias.Select(r => r.alunoId).Distinct().ToList();

            if (!alunoRepository.TodosAlunosExistem(idsAlunos))
                throw new InvalidOperationException(
                    "Um ou mais alunos informados não existem no banco de dados. Verifique a lista.");

            repository.Registrar(frequencias);
        }

        public List<FrequenciaResumoReadModel> ObterResumoFrequencia(int usuarioId)
        {
            var aluno = alunoRepository.ObterAlunoPorUsuarioId(usuarioId)
                ?? throw new KeyNotFoundException("Aluno não encontrado.");

            return repository.ObterResumoPorAluno(aluno.id, aluno.turmaId)
                ?? throw new KeyNotFoundException("Resumo de frequência não encontrado.");
        }
    }
}