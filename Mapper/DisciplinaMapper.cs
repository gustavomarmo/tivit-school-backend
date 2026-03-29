using edu_connect_backend.DTO;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class DisciplinaMapper
    {
        public DisciplinaMapper()
        {
        }

        public Disciplina ToDisciplina(DisciplinaCriacaoDTO dto)
        {
            return new Disciplina
            {
                nome = dto.nome,
                codigo = dto.codigo
            };
        }

        public TurmaDisciplina ToTurmaDisciplina(VincularDisciplinaDTO dto)
        {
            return new TurmaDisciplina
            {
                turmaId = dto.turmaId,
                disciplinaId = dto.disciplinaId,
                professorId = dto.professorId
            };
        }

        public DisciplinaResumoDTO ToDisciplinaResumoDTO(TurmaDisciplina model)
        {
            return new DisciplinaResumoDTO
            {
                id = model.id,
                disciplinaId = model.disciplinaId,
                turmaId = model.turmaId,
                nome = model.disciplina != null ? model.disciplina.nome : "N/A",
                turma = model.turma != null ? model.turma.nome : "N/A",
                professor = model.professor != null && model.professor.usuario != null
                            ? model.professor.usuario.nome
                            : "Sem Professor"
            };
        }

        public List<DisciplinaResumoDTO> ToDisciplinaResumoDTOList(List<TurmaDisciplina> models)
        {
            return models.Select(ToDisciplinaResumoDTO).ToList();
        }

        public DisciplinaConteudoDTO ToDisciplinaConteudoDTO(TurmaDisciplina model, List<int> materiaisEntreguesIds)
        {
            return new DisciplinaConteudoDTO
            {
                id = model.id,
                nome = model.disciplina.nome,
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
                        entregue = (m.tipo == "assignment") && materiaisEntreguesIds.Contains(m.id)
                    }).ToList()
                }).ToList()
            };
        }
    }
}