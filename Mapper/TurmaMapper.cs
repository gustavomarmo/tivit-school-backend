using edu_connect_backend.DTO.Turma;
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
                Id = model.id,
                Nome = model.nome,
                AnoLetivo = model.anoLetivo
            };
        }

        public List<TurmaResponseDTO> ToTurmaResponseDTOList(List<Turma> models)
        {
            return models.Select(ToTurmaResponseDTO).ToList();
        }
    }
}