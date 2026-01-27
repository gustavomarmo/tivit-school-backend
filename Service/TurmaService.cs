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

        public List<string> ListarNomesTurmas()
        {
            var turmas = this.turmaRepository.ListarTurmas();
            return turmas.Select(t => t.nome).Distinct().ToList();
        }
    }
}