using edu_connect_backend.DTO;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class TurmaMapper
    {
        public TurmaMapper()
        {
        }
        public List<TurmaResponseDTO> ToTurmaResponseDTOList(List<Turma> models)
        {
            return models.Select(t => new TurmaResponseDTO
            {
                id = t.id,
                nome = t.nome
            }).ToList();
        }
    }
}