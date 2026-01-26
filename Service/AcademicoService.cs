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
        private readonly AcademicoRepository academicoRepository;
        private readonly UsuarioRepository usuarioRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment env;

        public AcademicoService(
            ConnectionContext context,
            AcademicoRepository academicoRepository,
            UsuarioRepository usuarioRepository,
            AlunoRepository alunoRepository,
            ProfessorRepository professorRepository,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env
        )
        {
            this.academicoRepository = academicoRepository;
            this.usuarioRepository = usuarioRepository;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.env = env;
        }

        public void CriarDisciplinaGenerica(DisciplinaCriacaoDTO dto)
        {
            var nova = new Disciplina { nome = dto.nome, codigo = dto.codigo };
            academicoRepository.CriarDisciplina(nova);
        }

        public void VincularDisciplina(VincularDisciplinaDTO dto)
        {
            var vinculo = new TurmaDisciplina
            {
                turmaId = dto.turmaId,
                disciplinaId = dto.disciplinaId,
                professorId = dto.professorId
            };
            academicoRepository.vincularDisciplina(vinculo);
        }

        public List<string> ListarTurmas()
        {
            var turmas = academicoRepository.listarTurmas();

            return turmas.Select(t => t.nome).Distinct().ToList();
        }
        public List<DisciplinaResumoDTO> ListarDisciplinas(string emailUsuario)
        {
            var usuario = usuarioRepository.obterUsuarioPorEmail(emailUsuario);
            if (usuario == null) return new List<DisciplinaResumoDTO>();

            List<TurmaDisciplina> lista = new();

            if (usuario.perfil == PerfilUsuario.Aluno)
            {
                var aluno = context.alunos.FirstOrDefault(a => a.usuarioId == usuario.id);

                if (aluno != null)
                {
                    lista = academicoRepository.obterDisciplinasPorAluno(aluno.id);
                }
            }
            else if (usuario.perfil == PerfilUsuario.Professor)
            {
                var professor = context.professores.FirstOrDefault(p => p.usuarioId == usuario.id);

                if (professor != null)
                {
                    lista = academicoRepository.obterDisciplinasPorProfessor(professor.id);
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
            var turmaDisciplina = academicoRepository.obterConteudoCompleto(disciplinaId);
            if (turmaDisciplina == null) return null;

            var usuarioId = obterIdUsuarioLogado();

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

                        entregue = (m.tipo == "assignment") && academicoRepository.ExisteEntrega(m.id, usuarioId)
                    }).ToList()
                }).ToList()
            };
        }

        public void CriarTopico(TopicoRequestDTO dto)
        {
            var topico = new Topico { titulo = dto.titulo, turmaDisciplinaId = dto.turmaDisciplinaId };
            academicoRepository.CriarTopico(topico);
        }
        public void EditarTopico(int id, TopicoRequestDTO dto)
        {
            var topico = academicoRepository.ObterTopicoPorId(id);
            if (topico == null) throw new Exception("Tópico não encontrado.");

            // Atualizamos apenas o título
            topico.titulo = dto.titulo;

            academicoRepository.AtualizarTopico(topico);
        }

        public void DeletarTopico(int id)
        {
            var topico = academicoRepository.ObterTopicoPorId(id);
            if (topico == null) throw new Exception("Tópico não encontrado.");

            academicoRepository.DeletarTopico(topico);
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
            academicoRepository.AdicionarMaterial(material);
        }

        private int obterIdUsuarioLogado()
        {
            var idClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id)) return id;
            throw new Exception("Usuário não identificado.");
        }

        public void EditarMaterial(int id, MaterialRequestDTO dto)
        {
            var material = academicoRepository.ObterMaterialPorId(id);
            if (material == null) throw new Exception("Material não encontrado.");

            material.titulo = dto.titulo;
            material.url = dto.url;
            material.tipo = dto.tipo;
            material.topicoId = dto.topicoId;

            academicoRepository.AtualizarMaterial(material);
        }

        public void DeletarMaterial(int id)
        {
            var material = academicoRepository.ObterMaterialPorId(id);
            if (material == null) throw new Exception("Material não encontrado.");

            academicoRepository.DeletarMaterial(material);
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
            academicoRepository.AdicionarMaterial(atividade);
        }

        public void EditarAtividade(int id, AtividadeRequestDTO dto)
        {
            var material = academicoRepository.ObterMaterialPorId(id);
            if (material == null) throw new Exception("Atividade não encontrada.");
            if (material.tipo != "assignment") throw new Exception("Este item não é uma atividade.");

            material.titulo = dto.titulo;
            material.descricao = dto.descricao;
            material.dataEntrega = dto.dataEntrega;
            material.notaMaxima = dto.notaMaxima;

            academicoRepository.AtualizarMaterial(material);
        }

        public void DeletarAtividade(int id)
        {
            var material = academicoRepository.ObterMaterialPorId(id);
            academicoRepository.DeletarMaterial(material);
        }

        public string RegistrarEntrega(int atividadeId, IFormFile arquivo)
        {
            var usuarioId = obterIdUsuarioLogado();

            var aluno = context.alunos.FirstOrDefault(a => a.usuarioId == usuarioId);
            if (aluno == null) throw new Exception("Perfil de aluno não encontrado para este usuário.");

            var material = academicoRepository.ObterMaterialPorId(atividadeId);
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

            academicoRepository.AdicionarEntrega(novaEntrega);

            return novaEntrega.arquivoUrl;
        }

        public List<DisciplinaResumoDTO> listarExtracurriculares(string emailUsuario)
        {
            var usuario = usuarioRepository.obterUsuarioPorEmail(emailUsuario);
            if (usuario == null) return new List<DisciplinaResumoDTO>();

            List<TurmaExtracurricular> lista = new();

            if (usuario.perfil == PerfilUsuario.Aluno)
            {
                var aluno = context.alunos.FirstOrDefault(a => a.usuarioId == usuario.id);
                if (aluno != null)
                {
                    lista = academicoRepository.ObterExtracurricularesPorAluno(aluno.id);
                }
            }

            return lista.Select(te => new DisciplinaResumoDTO
            {
                id = te.id,
                nome = te.extracurricular.nome,
                turma = te.turma != null ? te.turma.nome : "N/A",
                professor = te.professor != null && te.professor.usuario != null
                            ? te.professor.usuario.nome
                            : "Instrutor"
            }).ToList();
        }

        public DisciplinaConteudoDTO? obterConteudoExtracurricular(int idVunculo)
        {
            var dados = academicoRepository.ObterConteudoExtracurricularCompleto(idVunculo);
            if (dados == null) return null;

            var usuarioId = obterIdUsuarioLogado();

            return new DisciplinaConteudoDTO
            {
                id = dados.id,
                nome = dados.extracurricular.nome,
                topicos = dados.topicos.Select(t => new TopicoDTO
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
                        entregue = (m.tipo == "assignment") && academicoRepository.ExisteEntrega(m.id, usuarioId)
                    }).ToList()
                }).ToList()
            };
        }

        public void criarAtividadeExtracurricular(ExtracurricularRequestDTO dto)
        {
            var nova = new Extracurricular
            {
                nome = dto.nome,
                descricao = dto.descricao
            };
            academicoRepository.CriarExtracurricular(nova);
        }

        public void editarAtividadeExtracurricular(int id, ExtracurricularRequestDTO dto)
        {
            var atividade = academicoRepository.ObterExtracurricularPorId(id);
            if (atividade == null) throw new Exception("Atividade não encontrada.");

            atividade.nome = dto.nome;
            atividade.descricao = dto.descricao;

            academicoRepository.AtualizarExtracurricular(atividade);
        }

        public void deletarAtividadeExtracurricular(int id)
        {
            var atividade = academicoRepository.ObterExtracurricularPorId(id);
            if (atividade == null) throw new Exception("Atividade não encontrada.");

            academicoRepository.DeletarExtracurricular(atividade);
        }

        public void vincularExtracurricular(VincularExtracurricularDTO dto)
        {
            var vinculo = new TurmaExtracurricular
            {
                turmaId = dto.turmaId,
                extracurricularId = dto.extracurricularId,
                professorId = dto.professorId
            };
            academicoRepository.VincularTurmaExtracurricular(vinculo);
        }

        public void removerVinculoExtracurricular(int idVinculo)
        {
            var vinculo = academicoRepository.ObterVinculoPorId(idVinculo);
            if (vinculo == null) throw new Exception("Vínculo não encontrado.");

            academicoRepository.DeletarVinculoExtracurricular(vinculo);
        }
    }
}