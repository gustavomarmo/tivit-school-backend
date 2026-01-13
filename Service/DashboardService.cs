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

        public DashboardService(
            DashboardRepository repository,
            IHttpContextAccessor httpContextAccessor,
            AlunoRepository alunoRepository,
            ProfessorRepository professorRepository,
            FrequenciaRepository frequenciaRepository,
            UsuarioRepository usuarioRepository)
        {
            this.repository = repository;
            this.alunoRepository = alunoRepository;
            this.usuarioRepository = usuarioRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.professorRepository = professorRepository;
            this.frequenciaRepository = frequenciaRepository;
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

            if (professor == null) throw new Exception("Perfil de professor não encontrado.");

            var dados = repository.ObterNotasPorProfessor(professor.id);

            var response = new DashboardProfessorResponseDTO
            {
                kpis = new KPIsProfessorDTO(),
                alunosAtencao = new List<AlunoAtencaoDTO>()
            };

            if (dados.Any())
            {
                response.kpis.mediaTurmas = Math.Round(dados.Average(d => d.Media), 1);

                response.kpis.alunosEmRec = dados.Count(d => d.Media < 6.0m);

                response.alunosAtencao = dados
                    .Where(d => d.Media < 6.0m)
                    .OrderBy(d => d.Media)
                    .Take(5)
                    .Select(d => new AlunoAtencaoDTO
                    {
                        id = d.AlunoId,
                        nome = d.AlunoNome,
                        turma = d.TurmaNome,
                        disciplina = d.DisciplinaNome,
                        media = Math.Round(d.Media, 1),
                        foto = ""
                    }).ToList();
            }

            response.kpis.frequencia = frequenciaRepository.ObterPercentualFrequenciaProfessor(professor.id);

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