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

            var dadosBrutos = repository.ObterDadosBrutosPorAluno(aluno.id, aluno.turmaId);

            return dadosBrutos.Select(item => new FrequenciaResumoReadModel
            {
                disciplina = item.nomeDisciplina,
                totalAulas = item.totalAulas,
                totalFaltas = item.totalAulas - item.presencas,
                frequencia = item.totalAulas == 0
                    ? 100
                    : (double)Math.Round(((decimal)item.presencas / item.totalAulas) * 100, 1)
            }).ToList();
        }
    }
}