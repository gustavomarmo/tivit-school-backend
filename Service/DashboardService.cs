using edu_connect_backend.DTO;
using edu_connect_backend.Repository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace edu_connect_backend.Service
{
    public class DashboardService
    {
        private readonly DashboardRepository repository;
        private readonly AlunoRepository alunoRepository;
        private readonly UsuarioRepository usuarioRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ProfessorRepository professorRepository;
        private readonly FrequenciaRepository frequenciaRepository;
        private readonly EventoRepository eventoRepository;

        public DashboardService(
            DashboardRepository repository,
            IHttpContextAccessor httpContextAccessor,
            AlunoRepository alunoRepository,
            ProfessorRepository professorRepository,
            FrequenciaRepository frequenciaRepository,
            UsuarioRepository usuarioRepository,
            EventoRepository eventoRepository)
        {
            this.repository = repository;
            this.alunoRepository = alunoRepository;
            this.usuarioRepository = usuarioRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.professorRepository = professorRepository;
            this.frequenciaRepository = frequenciaRepository;
            this.eventoRepository = eventoRepository;
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

        public DashboardProfessorResponseDTO ObterDashboardProfessor()
        {
            var idUsuario = ObterIdUsuarioLogado();
            var professor = professorRepository.ObterPorUsuarioId(idUsuario);

            if (professor == null) throw new Exception("Professor não encontrado.");

            var kpis = repository.ObterKPIsProfessorProcedure(professor.id);

            var alunosAtencao = repository.ObterAlunosEmRisco(professor.id);

            return new DashboardProfessorResponseDTO
            {
                kpis = kpis,
                alunosAtencao = alunosAtencao
            };
        }

        public DashboardCoordenadorResponseDTO ObterDashboardCoordenador()
        {
            var response = new DashboardCoordenadorResponseDTO();

            response.kpis = repository.ObterKPIsCoordenador();

            response.graficoDesempenho = repository.ObterGraficoDesempenhoTurmas();
            response.graficoStatus = repository.ObterGraficoStatusAlunos();

            var eventosFuturos = eventoRepository.ObterProximosEventos(5);

            response.proximosEventos = eventosFuturos.Select(e => new EventoResponseDTO
            {
                id = e.id,
                title = e.titulo,
                start = e.dataInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                type = e.tipo,
                description = e.descricao,
                turmaNome = e.turma != null ? e.turma.nome : "Geral"
            }).ToList();

            return response;
        }

        private int ObterIdUsuarioLogado()
        {
            var idClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id)) return id;
            throw new Exception("Usuário não identificado.");
        }
    }
}