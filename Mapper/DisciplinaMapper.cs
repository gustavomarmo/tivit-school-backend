using edu_connect_backend.DTO.Disciplina;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class DisciplinaMapper
    {
        public DisciplinaMapper()
        {
        }

        public Disciplina ToDisciplina(DisciplinaCriacaoResponseDTO dto)
        {
            return new Disciplina
            {
                nome = dto.Nome,
                codigo = dto.Codigo
            };
        }

        public TurmaDisciplina ToTurmaDisciplina(VincularDisciplinaRequestDTO dto)
        {
            return new TurmaDisciplina
            {
                turmaId = dto.TurmaId,
                disciplinaId = dto.DisciplinaId,
                professorId = dto.ProfessorId
            };
        }

        public DisciplinaResumoDTO ToDisciplinaResumoDTO(TurmaDisciplina model)
        {
            return new DisciplinaResumoDTO
            {
                Id = model.id,
                DisciplinaId = model.disciplinaId,
                TurmaId = model.turmaId,
                Nome = model.disciplina != null ? model.disciplina.nome : "N/A",
                Turma = model.turma != null ? model.turma.nome : "N/A",
                Professor = model.professor != null && model.professor.usuario != null
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
                Id = model.id,
                Nome = model.disciplina.nome,
                Topicos = model.topicos.Select(t => new TopicoDTO
                {
                    Id = t.id,
                    Titulo = t.titulo,
                    Materiais = t.materiais.Select(m => new MaterialDTO
                    {
                        Id = m.id,
                        Titulo = m.titulo,
                        Tipo = m.tipo,
                        Url = m.url,
                        DataEntrega = m.dataEntrega,
                        Entregue = (m.tipo == "assignment") && materiaisEntreguesIds.Contains(m.id)
                    }).ToList()
                }).ToList()
            };
        }
    }
}