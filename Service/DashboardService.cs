using edu_connect_backend.DTO;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class DashboardService
    {
        private readonly DashboardRepository repository;
        private readonly AlunoRepository alunoRepository;
        private readonly UsuarioRepository usuarioRepository;

        public DashboardService(
            DashboardRepository repository,
            AlunoRepository alunoRepository,
            UsuarioRepository usuarioRepository)
        {
            this.repository = repository;
            this.alunoRepository = alunoRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public DashboardAlunoDTO? ObterDashboardAluno(string emailUsuario)
        {
            var usuario = usuarioRepository.ObterPorEmail(emailUsuario);
            if (usuario == null) return null;

            var aluno = alunoRepository.ObterAlunos(usuario.nome)
                            .FirstOrDefault(a => a.usuarioId == usuario.id);

            if (aluno == null || aluno.turmaId == null) return null;

            var dashboard = new DashboardAlunoDTO
            {
                ultimasNotas = repository.ObterNotasRecentes(aluno.id),
                avisos = repository.ObterAvisos(aluno.turmaId.Value),
                tarefasPendentes = repository.ObterTarefasPendentes(aluno.turmaId.Value)
            };

            return dashboard;
        }
    }
}