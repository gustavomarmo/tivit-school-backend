using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Mapper
{
    public class ExtracurricularMapper
    {
        private readonly AtividadeRepository atividadeRepository;

        public ExtracurricularMapper(AtividadeRepository atividadeRepository)
        {
            this.atividadeRepository = atividadeRepository;
        }

        public Extracurricular ToExtracurricular(ExtracurricularRequestDTO dto)
        {
            return new Extracurricular
            {
                nome = dto.nome,
                descricao = dto.descricao
            };
        }

        public TurmaExtracurricular ToTurmaExtracurricular(VincularExtracurricularDTO dto)
        {
            return new TurmaExtracurricular
            {
                turmaId = dto.turmaId,
                extracurricularId = dto.extracurricularId,
                professorId = dto.professorId
            };
        }

        public List<DisciplinaResumoDTO> ToDisciplinaResumoDTOList(List<TurmaExtracurricular> models)
        {
            return models.Select(te => new DisciplinaResumoDTO
            {
                id = te.id,
                nome = te.extracurricular.nome,
                turma = te.turma != null ? te.turma.nome : "N/A",
                professor = te.professor != null && te.professor.usuario != null
                            ? te.professor.usuario.nome
                            : "Instrutor"
            }).ToList();
        }

        public DisciplinaConteudoDTO ToDisciplinaConteudoDTO(TurmaExtracurricular model, int usuarioId)
        {
            return new DisciplinaConteudoDTO
            {
                id = model.id,
                nome = model.extracurricular.nome,
                topicos = model.topicos.Select(t => new TopicoDTO
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
                        entregue = (m.tipo == "assignment") && atividadeRepository.ExisteEntrega(m.id, usuarioId)
                    }).ToList()
                }).ToList()
            };
        }
    }
}