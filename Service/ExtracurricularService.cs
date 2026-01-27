using edu_connect_backend.Context;
using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace edu_connect_backend.Service
{
    public class ExtracurricularService
    {
        private readonly ExtracurricularRepository extracurricularRepository;
        private readonly UsuarioRepository usuarioRepository;
        private readonly ConnectionContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AtividadeRepository atividadeRepository;

        public ExtracurricularService(
            ExtracurricularRepository extracurricularRepository,
            UsuarioRepository usuarioRepository,
            ConnectionContext context,
            IHttpContextAccessor httpContextAccessor,
            AtividadeRepository atividadeRepository)
        {
            this.extracurricularRepository = extracurricularRepository;
            this.usuarioRepository = usuarioRepository;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.atividadeRepository = atividadeRepository;
        }

        public List<DisciplinaResumoDTO> ListarExtracurriculares(string emailUsuario)
        {
            var usuario = this.usuarioRepository.ObterUsuarioPorEmail(emailUsuario);
            if (usuario == null) return new List<DisciplinaResumoDTO>();

            List<TurmaExtracurricular> lista = new();

            if (usuario.perfil == PerfilUsuario.Aluno)
            {
                var aluno = this.context.alunos.FirstOrDefault(a => a.usuarioId == usuario.id);
                if (aluno != null)
                {
                    lista = this.extracurricularRepository.ObterExtracurricularesPorAluno(aluno.id);
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

        public DisciplinaConteudoDTO? ObterConteudoExtracurricular(int idVunculo)
        {
            var dados = this.extracurricularRepository.ObterConteudoCompleto(idVunculo);
            if (dados == null) return null;

            var usuarioId = ObterIdUsuarioLogado();

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
                        entregue = (m.tipo == "assignment") && this.atividadeRepository.ExisteEntrega(m.id, usuarioId)
                    }).ToList()
                }).ToList()
            };
        }

        public void CriarAtividadeExtracurricular(ExtracurricularRequestDTO dto)
        {
            var nova = new Extracurricular
            {
                nome = dto.nome,
                descricao = dto.descricao
            };
            extracurricularRepository.CriarExtracurricular(nova);
        }

        public void EditarAtividadeExtracurricular(int id, ExtracurricularRequestDTO dto)
        {
            var atividade = extracurricularRepository.ObterExtracurricularPorId(id);
            if (atividade == null) throw new Exception("Atividade não encontrada.");

            atividade.nome = dto.nome;
            atividade.descricao = dto.descricao;

            extracurricularRepository.AtualizarExtracurricular(atividade);
        }

        public void DeletarAtividadeExtracurricular(int id)
        {
            var atividade = extracurricularRepository.ObterExtracurricularPorId(id);
            if (atividade == null) throw new Exception("Atividade não encontrada.");

            extracurricularRepository.DeletarExtracurricular(atividade);
        }

        public void VincularExtracurricular(VincularExtracurricularDTO dto)
        {
            var vinculo = new TurmaExtracurricular
            {
                turmaId = dto.turmaId,
                extracurricularId = dto.extracurricularId,
                professorId = dto.professorId
            };
            extracurricularRepository.VincularTurmaExtracurricular(vinculo);
        }

        public void RemoverVinculoExtracurricular(int idVinculo)
        {
            var vinculo = extracurricularRepository.ObterVinculoPorId(idVinculo);
            if (vinculo == null) throw new Exception("Vínculo não encontrado.");

            extracurricularRepository.DeletarVinculoExtracurricular(vinculo);
        }

        private int ObterIdUsuarioLogado()
        {
            var idClaim = this.httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id)) return id;
            throw new Exception("Usuário não identificado.");
        }
    }
}