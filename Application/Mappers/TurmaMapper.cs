using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Domain.Entities;

namespace edu_connect_backend.Application.Mappers
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