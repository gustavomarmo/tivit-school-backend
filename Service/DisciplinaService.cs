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
        private readonly ProfessorRepository professorRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AtividadeRepository atividadeRepository;
        private readonly NotificacaoRepository notificacaoRepository;

        public DisciplinaService(
            DisciplinaRepository disciplinaRepository,
            UsuarioRepository usuarioRepository,
            AlunoRepository alunoRepository,
            ProfessorRepository professorRepository,
            IHttpContextAccessor httpContextAccessor,
            AtividadeRepository atividadeRepository,
            NotificacaoRepository notificacaoRepository)
        {
            this.disciplinaRepository = disciplinaRepository;
            this.usuarioRepository = usuarioRepository;
            this.alunoRepository = alunoRepository;
            this.professorRepository = professorRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.atividadeRepository = atividadeRepository;
            this.notificacaoRepository = notificacaoRepository;
        }

        public void CriarDisciplinaGenerica(Disciplina disciplina)
        {
            disciplinaRepository.CriarDisciplina(disciplina);
        }

        public void VincularDisciplina(TurmaDisciplina vinculo)
        {
            disciplinaRepository.VincularDisciplina(vinculo);

            var professor = professorRepository.ObterPorId(vinculo.professorId);

            if (professor?.usuarioId != null)
            {
                notificacaoRepository.CriarParaUsuario(
                    usuarioId: professor.usuarioId,
                    tipo: "success",
                    titulo: "Nova Turma Vinculada",
                    mensagem: $"Você foi vinculado a uma nova disciplina/turma. Acesse Matérias para mais detalhes."
                );
            }
        }

        public List<TurmaDisciplina> ListarDisciplinas(string emailUsuario)
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmail(emailUsuario)
                 ?? throw new KeyNotFoundException("Usuário não encontrado.");

            if (usuario.perfil == PerfilUsuario.Aluno)
            {
                var aluno = alunoRepository.ObterAlunoPorUsuarioId(usuario.id)
                    ?? throw new KeyNotFoundException("Aluno não encontrado.");

                return disciplinaRepository.ObterDisciplinasPorAlunoId(aluno.id)
                    ?? throw new KeyNotFoundException("Nenhuma disciplina encontrada do aluno.");
            }
            else if (usuario.perfil == PerfilUsuario.Professor)
            {
                var professor = professorRepository.ObterProfessorPorUsuarioId(usuario.id)
                    ?? throw new KeyNotFoundException("Professor não encontrado.");
                return disciplinaRepository.ObterDisciplinasPorProfessorId(professor.id)
                    ?? throw new KeyNotFoundException("Nenhuma disciplina encontrada do professor.");
            }

            return new List<TurmaDisciplina>();
        }

        public List<Disciplina> ListarTodasDisciplinasGenericas()
        {
            return disciplinaRepository.ListarTodasDisciplinas();
        }

        public (TurmaDisciplina? disciplina, List<int> entregues) ObterConteudoDisciplina(int disciplinaId, string emailUsuario)
        {
            var turmaDisciplina = disciplinaRepository.ObterConteudoCompleto(disciplinaId)
                ?? throw new KeyNotFoundException("Nenhum conteúdo encontrado da disciplina");

            var usuario = usuarioRepository.ObterUsuarioPorEmail(emailUsuario)
                ?? throw new KeyNotFoundException("Usuário não encontrado.");

            if (usuario.perfil != PerfilUsuario.Aluno)
                return (turmaDisciplina, new List<int>());

            var idAluno = GetAlunoId(usuario.id);
            var entregues = atividadeRepository.ObterIdsMateriaisEntregues(disciplinaId, idAluno);

            return (turmaDisciplina, entregues);
        }

        private int GetAlunoId(int usuarioId)
        {
            var aluno = alunoRepository.ObterAlunoPorUsuarioId(usuarioId)
                ?? throw new KeyNotFoundException("Aluno não encontrado.");
            return aluno.id;
        }
    }
}