using edu_connect_backend.Context;
using edu_connect_backend.DTO;
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
        private readonly ConnectionContext context; // Para acessar Professores diretamente se necessário, ou crie ProfessorRepository
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AtividadeRepository atividadeRepository; // Para checar entregas ao listar conteúdo

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

        public void CriarDisciplinaGenerica(DisciplinaCriacaoDTO dto)
        {
            var nova = new Disciplina { nome = dto.nome, codigo = dto.codigo };
            this.disciplinaRepository.CriarDisciplina(nova);
        }

        public void VincularDisciplina(VincularDisciplinaDTO dto)
        {
            var vinculo = new TurmaDisciplina
            {
                turmaId = dto.turmaId,
                disciplinaId = dto.disciplinaId,
                professorId = dto.professorId
            };
            this.disciplinaRepository.VincularDisciplina(vinculo);
        }

        public List<DisciplinaResumoDTO> ListarDisciplinas(string emailUsuario)
        {
            var usuario = this.usuarioRepository.ObterUsuarioPorEmail(emailUsuario);
            if (usuario == null) return new List<DisciplinaResumoDTO>();

            List<TurmaDisciplina> lista = new();

            if (usuario.perfil == PerfilUsuario.Aluno)
            {
                var aluno = this.alunoRepository.ObterAlunoPorUsuarioId(usuario.id);
                if (aluno != null)
                {
                    lista = this.disciplinaRepository.ObterDisciplinasPorAlunoId(aluno.id);
                }
            }
            else if (usuario.perfil == PerfilUsuario.Professor)
            {
                var professor = this.context.professores.FirstOrDefault(p => p.usuarioId == usuario.id);
                if (professor != null)
                {
                    lista = this.disciplinaRepository.ObterDisciplinasPorProfessorId(professor.id);
                }
            }

            return lista.Select(td => new DisciplinaResumoDTO
            {
                id = td.id,
                nome = td.disciplina.nome,
                turma = td.turma != null ? td.turma.nome : "N/A",
                professor = td.professor != null && td.professor.usuario != null
                            ? td.professor.usuario.nome
                            : "Sem Professor"
            }).ToList();
        }

        public DisciplinaConteudoDTO? ObterConteudo(int disciplinaId)
        {
            var turmaDisciplina = this.disciplinaRepository.ObterConteudoCompleto(disciplinaId);
            if (turmaDisciplina == null) return null;

            var usuarioId = ObterIdUsuarioLogado();

            return new DisciplinaConteudoDTO
            {
                id = turmaDisciplina.id,
                nome = turmaDisciplina.disciplina.nome,
                topicos = turmaDisciplina.topicos.Select(t => new TopicoDTO
                {
                    id = t.id,
                    titulo = t.titulo,
                    materiais = t.materiais.Select(m => new MaterialDTO
                    {
                        id = m.id,
                        titulo = m.titulo,
                        tipo = m.tipo,
                        url = m.url,
                        dataEntrega = m.dataEntrega,
                        entregue = (m.tipo == "assignment") && this.atividadeRepository.ExisteEntrega(m.id, usuarioId)
                    }).ToList()
                }).ToList()
            };
        }

        private int ObterIdUsuarioLogado()
        {
            var idClaim = this.httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id)) return id;
            throw new Exception("Usuário não identificado.");
        }
    }
}