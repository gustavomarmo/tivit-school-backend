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
            return this.turmaRepository.ListarTurmas()
                ?? throw new KeyNotFoundException("Turmas não encontradas");
        }
    }
}