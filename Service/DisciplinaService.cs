using edu_connect_backend.Context;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace edu_connect_backend.Service
{
    public class DisciplinaService
    {
        private readonly DisciplinaRepository disciplinaRepository;
        private readonly UsuarioRepository usuarioRepository;
        private readonly AlunoRepository alunoRepository;
        private readonly ConnectionContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AtividadeRepository atividadeRepository;

        public DisciplinaService(
            DisciplinaRepository disciplinaRepository,
            UsuarioRepository usuarioRepository,
            AlunoRepository alunoRepository,
            ConnectionContext context,
            IHttpContextAccessor httpContextAccessor,
            AtividadeRepository atividadeRepository)
        {
            this.disciplinaRepository = disciplinaRepository;
            this.usuarioRepository = usuarioRepository;
            this.alunoRepository = alunoRepository;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.atividadeRepository = atividadeRepository;
        }

        public void CriarDisciplinaGenerica(Disciplina disciplina)
        {
            this.disciplinaRepository.CriarDisciplina(disciplina);
        }

        public void VincularDisciplina(TurmaDisciplina vinculo)
        {
            this.disciplinaRepository.VincularDisciplina(vinculo);
        }

        public List<TurmaDisciplina> ListarDisciplinas(string emailUsuario)
        {
            var usuario = this.usuarioRepository.ObterUsuarioPorEmail(emailUsuario);
            if (usuario == null) return new List<TurmaDisciplina>();

            if (usuario.perfil == PerfilUsuario.Aluno)
            {
                var aluno = this.alunoRepository.ObterAlunoPorUsuarioId(usuario.id);
                if (aluno != null)
                {
                    return this.disciplinaRepository.ObterDisciplinasPorAlunoId(aluno.id);
                }
            }
            else if (usuario.perfil == PerfilUsuario.Professor)
            {
                var professor = this.context.professores.FirstOrDefault(p => p.usuarioId == usuario.id);
                if (professor != null)
                {
                    return this.disciplinaRepository.ObterDisciplinasPorProfessorId(professor.id);
                }
            }

            return new List<TurmaDisciplina>();
        }

        public (TurmaDisciplina? disciplina, List<int> entregues) ObterConteudoDisciplina(int disciplinaId)
        {
            var turmaDisciplina = this.disciplinaRepository.ObterConteudoCompleto(disciplinaId);
            if (turmaDisciplina == null) return (null, new List<int>());

            var usuarioId = ObterIdUsuarioLogado();

            var entregues = this.atividadeRepository.ObterIdsMateriaisEntregues(disciplinaId, GetAlunoId(usuarioId));

            return (turmaDisciplina, entregues);
        }

        private int ObterIdUsuarioLogado()
        {
            var idClaim = this.httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id)) return id;
            throw new Exception("Usuário não identificado.");
        }

        private int GetAlunoId(int usuarioId)
        {
            var aluno = this.alunoRepository.ObterAlunoPorUsuarioId(usuarioId);
            return aluno != null ? aluno.id : 0;
        }
    }
}