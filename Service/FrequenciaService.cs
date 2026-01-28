using edu_connect_backend.Context;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
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

            if (!this.alunoRepository.TodosAlunosExistem(idsAlunos))
            {
                throw new Exception("Um ou mais alunos informados não existem no banco de dados. Verifique a lista.");
            }

            this.repository.Registrar(frequencias);
        }

        public List<FrequenciaResumoReadModel> ObterResumoFrequencia(int usuarioId)
        {
            var aluno = this.context.alunos.FirstOrDefault(a => a.usuarioId == usuarioId);

            if (aluno == null)
                throw new Exception("Perfil de aluno não encontrado para este usuário.");

            return repository.ObterResumoPorAluno(aluno.id, aluno.turmaId);
        }
    }
}