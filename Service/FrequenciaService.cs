using edu_connect_backend.Context;
using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using System.Security.Claims;

namespace edu_connect_backend.Service
{
    public class FrequenciaService
    {
        private readonly FrequenciaRepository repository;
        private readonly AlunoRepository alunoRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ConnectionContext context;

        public FrequenciaService(
            FrequenciaRepository repository,
            AlunoRepository alunoRepository,
            IHttpContextAccessor httpContextAccessor,
            ConnectionContext context)
        {
            this.repository = repository;
            this.alunoRepository = alunoRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
        }

        public void RealizarChamada(ChamadaRequestDTO dto)
        {
            var idsAlunos = dto.registros.Select(r => r.alunoId).ToList();

            if (!alunoRepository.TodosAlunosExistem(idsAlunos))
            {
                throw new Exception("Um ou mais alunos informados não existem no banco de dados. Verifique a lista.");
            }

            var listaParaSalvar = new List<Frequencia>();

            foreach (var registro in dto.registros)
            {
                listaParaSalvar.Add(new Frequencia
                {
                    dataAula = dto.data,
                    disciplinaId = dto.disciplinaId,
                    alunoId = registro.alunoId,
                    presente = registro.presente
                });
            }

            repository.Registrar(listaParaSalvar);
        }

        public List<FrequenciaResumoDTO> ObterResumoFrequenciaLogado()
        {
            var idClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null || !int.TryParse(idClaim.Value, out int usuarioId))
            {
                throw new Exception("Usuário não identificado no token.");
            }

            var aluno = context.alunos.FirstOrDefault(a => a.usuarioId == usuarioId);

            if (aluno == null)
                throw new Exception("Perfil de aluno não encontrado para este usuário.");

            return repository.ObterResumoPorAluno(aluno.id, aluno.turmaId);
        }
    }
}