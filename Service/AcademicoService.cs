using edu_connect_backend.Context;
using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace edu_connect_backend.Service
{
    public class AcademicoService
    {
        private readonly ConnectionContext context;
        private readonly AcademicoRepository repository;
        private readonly UsuarioRepository usuarioRepository;
        private readonly AlunoRepository alunoRepository;
        private readonly ProfessorRepository professorRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment env;

        public AcademicoService(
            ConnectionContext context,
            AcademicoRepository repository,
            UsuarioRepository usuarioRepository,
            AlunoRepository alunoRepository,
            ProfessorRepository professorRepository,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env
        )
        {
            this.repository = repository;
            this.usuarioRepository = usuarioRepository;
            this.alunoRepository = alunoRepository;
            this.professorRepository = professorRepository;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.env = env;
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

        public List<string> ListarTurmas()
        {
            var turmas = repository.ListarTodasTurmas();

            return turmas.Select(t => t.nome).Distinct().ToList();
        }
        public List<DisciplinaResumoDTO> ListarMinhasDisciplinas(string emailUsuario)
        {
            var usuario = usuarioRepository.ObterPorEmail(emailUsuario);
            if (usuario == null) return new List<DisciplinaResumoDTO>();

            List<TurmaDisciplina> lista = new();

            if (usuario.perfil == PerfilUsuario.Aluno)
            {
                var aluno = context.alunos.FirstOrDefault(a => a.usuarioId == usuario.id);

                if (aluno != null)
                {
                    lista = repository.ObterDisciplinasPorAluno(aluno.id);
                }
            }
            else if (usuario.perfil == PerfilUsuario.Professor)
            {
                var professor = context.professores.FirstOrDefault(p => p.usuarioId == usuario.id);

                if (professor != null)
                {
                    lista = repository.ObterDisciplinasPorProfessor(professor.id);
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
            var turmaDisciplina = repository.ObterConteudoCompleto(disciplinaId);
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

                        entregue = (m.tipo == "assignment") && repository.ExisteEntrega(m.id, usuarioId)
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

        private int ObterIdUsuarioLogado()
        {
            var idClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id)) return id;
            throw new Exception("Usuário não identificado.");
        }

        public void AtualizarMaterial(int id, MaterialRequestDTO dto)
        {
            var material = repository.ObterMaterialPorId(id);
            if (material == null) throw new Exception("Material não encontrado.");

            material.titulo = dto.titulo;
            material.url = dto.url;
            material.tipo = dto.tipo;
            material.topicoId = dto.topicoId;

            repository.AtualizarMaterial(material);
        }

        public void DeletarMaterial(int id)
        {
            var material = repository.ObterMaterialPorId(id);
            if (material == null) throw new Exception("Material não encontrado.");

            repository.DeletarMaterial(material);
        }

        public void CriarAtividade(AtividadeRequestDTO dto)
        {
            var atividade = new Material
            {
                titulo = dto.titulo,
                descricao = dto.descricao,
                tipo = "assignment",
                url = "",
                topicoId = dto.topicoId,
                dataEntrega = dto.dataEntrega,
                notaMaxima = dto.notaMaxima
            };
            repository.AdicionarMaterial(atividade);
        }

        public void AtualizarAtividade(int id, AtividadeRequestDTO dto)
        {
            var material = repository.ObterMaterialPorId(id);
            if (material == null) throw new Exception("Atividade não encontrada.");
            if (material.tipo != "assignment") throw new Exception("Este item não é uma atividade.");

            material.titulo = dto.titulo;
            material.descricao = dto.descricao;
            material.dataEntrega = dto.dataEntrega;
            material.notaMaxima = dto.notaMaxima;

            repository.AtualizarMaterial(material);
        }
        public string RegistrarEntrega(int atividadeId, IFormFile arquivo)
        {
            var usuarioId = ObterIdUsuarioLogado();

            var aluno = context.alunos.FirstOrDefault(a => a.usuarioId == usuarioId);
            if (aluno == null) throw new Exception("Perfil de aluno não encontrado para este usuário.");

            var material = repository.ObterMaterialPorId(atividadeId);
            if (material == null) throw new Exception("Atividade não encontrada.");

            if (material.tipo != "assignment" && material.dataEntrega == null)
                throw new Exception("Este material não é uma atividade.");

            if (arquivo == null || arquivo.Length == 0) throw new Exception("Arquivo inválido.");

            string pastaUploads = Path.Combine(env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads");

            if (!Directory.Exists(pastaUploads))
                Directory.CreateDirectory(pastaUploads);

            string nomeArquivo = $"{DateTime.Now.Ticks}_{arquivo.FileName}";
            string caminhoCompleto = Path.Combine(pastaUploads, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                arquivo.CopyTo(stream);
            }

            var novaEntrega = new Entrega
            {
                materialId = atividadeId,
                alunoId = aluno.id,
                arquivoUrl = $"/uploads/{nomeArquivo}",
                dataEntrega = DateTime.Now
            };

            repository.AdicionarEntrega(novaEntrega);

            return novaEntrega.arquivoUrl;
        }
    }
}