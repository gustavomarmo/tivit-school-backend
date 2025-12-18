using edu_connect_backend.Context;
using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class AcademicoService
    {
        private readonly ConnectionContext context;
        private readonly AcademicoRepository repository;
        private readonly UsuarioRepository usuarioRepository;
        private readonly AlunoRepository alunoRepository;
        private readonly ProfessorRepository professorRepository;

        public AcademicoService(ConnectionContext context,
            AcademicoRepository repository,
            UsuarioRepository usuarioRepository,
            AlunoRepository alunoRepository,
            ProfessorRepository professorRepository)
        {
            this.repository = repository;
            this.usuarioRepository = usuarioRepository;
            this.alunoRepository = alunoRepository;
            this.professorRepository = professorRepository;
            this.context = context;
        }

        public void CriarDisciplinaGenerica(DisciplinaCriacaoDTO dto)
        {
            var nova = new Disciplina { nome = dto.nome, codigo = dto.codigo };
            repository.CriarDisciplina(nova);
        }

        public void VincularDisciplina(VincularDisciplinaDTO dto)
        {
            var vinculo = new TurmaDisciplina
            {
                turmaId = dto.turmaId,
                disciplinaId = dto.disciplinaId,
                professorId = dto.professorId
            };
            repository.VincularDisciplinaTurma(vinculo);
        }
        public List<DisciplinaResumoDTO> ListarMinhasDisciplinas(string emailUsuario)
        {
            var usuario = usuarioRepository.ObterPorEmail(emailUsuario);
            if (usuario == null) return new List<DisciplinaResumoDTO>();

            List<TurmaDisciplina> lista = new();

            return lista.Select(td => new DisciplinaResumoDTO
            {
                id = td.id,
                nome = td.disciplina.nome,
                turma = td.turma.nome,
                professor = td.professor.usuario.nome
            }).ToList();
        }

        public DisciplinaConteudoDTO? ObterConteudo(int disciplinaId)
        {
            var turmaDisciplina = repository.ObterConteudoCompleto(disciplinaId);
            if (turmaDisciplina == null) return null;

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
                        url = m.url
                    }).ToList()
                }).ToList()
            };
        }

        public void CriarTopico(TopicoRequestDTO dto)
        {
            var topico = new Topico { titulo = dto.titulo, turmaDisciplinaId = dto.turmaDisciplinaId };
            repository.AdicionarTopico(topico);
        }

        public void CriarMaterial(MaterialRequestDTO dto)
        {
            var material = new Material
            {
                titulo = dto.titulo,
                tipo = dto.tipo,
                url = dto.url,
                topicoId = dto.topicoId,
                descricao = ""
            };
            repository.AdicionarMaterial(material);
        }

        // Simulação de entrega (apenas logica visual por enquanto)
        public void RegistrarEntrega(int atividadeId, string urlArquivo)
        {
            // Aqui você salvaria numa tabela 'Entrega' no futuro
        }
    }
}