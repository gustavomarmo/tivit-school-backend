using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Math.EC;
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

        public DashboardAlunoReadModel obterDashboardAluno(int usuarioId)
        {
            var usuario = usuarioRepository.obterPorUsuarioId(usuarioId)
                ?? throw new KeyNotFoundException($"Usuário não encontrado com id {usuarioId}");

            var aluno = alunoRepository.obterAlunos(usuario.nome)
                            .FirstOrDefault(a => a.usuarioId == usuario.id)
                            ?? throw new KeyNotFoundException("Cadastro de aluno não encontrado");
                                                                    
            var dashboard = new DashboardAlunoReadModel
            {
                ultimasNotas = repository.obterNotasRecentes(aluno.id),
                avisos = repository.obterAvisos(aluno.turmaId.Value),
                tarefasPendentes = repository.obterTarefasPendentes(aluno.turmaId.Value)
            };

            return dashboard;
        }

        public DashboardProfessorResponseDTO ObterDashboardProfessor(int usuarioId)
        {
            var usuario = usuarioRepository.obterPorUsuarioId(usuarioId)
                ?? throw new KeyNotFoundException($"Usuário não encontrado com id {usuarioId}");

            var professor = professorRepository.obterProfessorPorUsuarioId(usuario.id);

            var kpis = repository.obterKPIsProfessorProcedure(professor.id);

            var alunosAtencao = repository.obterAlunosEmRisco(professor.id);

            return new DashboardProfessorResponseDTO
            {
                kpis = kpis,
                alunosAtencao = alunosAtencao
            };
        }

        public DashboardCoordenadorResponseDTO ObterDashboardCoordenador()
        {
            var response = new DashboardCoordenadorResponseDTO();

            response.kpis = repository.obterKPIsCoordenador();

            response.graficoDesempenho = repository.obterGraficoDesempenhoTurmas();
            response.graficoStatus = repository.obterGraficoStatusAlunos();

            var eventosFuturos = eventoRepository.obterProximosEventos(5);

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

        
    }
}