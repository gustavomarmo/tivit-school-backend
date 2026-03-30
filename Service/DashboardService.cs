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
        private readonly ProfessorRepository professorRepository;
        private readonly FrequenciaRepository frequenciaRepository;
        private readonly EventoRepository eventoRepository;

        public DashboardService(
            DashboardRepository repository,
            AlunoRepository alunoRepository,
            ProfessorRepository professorRepository,
            FrequenciaRepository frequenciaRepository,
            UsuarioRepository usuarioRepository,
            EventoRepository eventoRepository)
        {
            this.repository = repository;
            this.alunoRepository = alunoRepository;
            this.usuarioRepository = usuarioRepository;
            this.professorRepository = professorRepository;
            this.frequenciaRepository = frequenciaRepository;
            this.eventoRepository = eventoRepository;
        }

        public DashboardAlunoReadModel ObterDashboardAluno(int usuarioId)
        {
            var usuario = usuarioRepository.ObterPorUsuarioId(usuarioId)
                ?? throw new KeyNotFoundException($"Usuário não encontrado com id {usuarioId}");

            var aluno = alunoRepository.ObterAlunos(usuario.nome)
                            .FirstOrDefault(a => a.usuarioId == usuario.id)
                            ?? throw new KeyNotFoundException("Cadastro de aluno não encontrado");
                                                                    
            var dashboard = new DashboardAlunoReadModel
            {
                ultimasNotas = repository.ObterNotasRecentes(aluno.id),
                avisos = repository.ObterAvisos(aluno.turmaId.Value),
                tarefasPendentes = repository.ObterTarefasPendentes(aluno.turmaId.Value)
            };

            return dashboard;
        }

        public DashboardProfessorResponseDTO ObterDashboardProfessor(int usuarioId)
        {
            var usuario = usuarioRepository.ObterPorUsuarioId(usuarioId)
                ?? throw new KeyNotFoundException($"Usuário não encontrado com id {usuarioId}");

            var professor = professorRepository.ObterProfessorPorUsuarioId(usuario.id);

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

        
    }
}