using edu_connect_backend.DTO;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class TurmaMapper
    {
        public TurmaMapper() { }

        public TurmaResponseDTO ToTurmaResponseDTO(Turma model)
        {
            return new TurmaResponseDTO
            {
                id = model.id,
                nome = model.nome,
                anoLetivo = model.anoLetivo
            };
        }

        public List<TurmaResponseDTO> ToTurmaResponseDTOList(List<Turma> models)
        {
            return models.Select(ToTurmaResponseDTO).ToList();
        }
    }
}