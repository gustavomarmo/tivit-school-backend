using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Domain.Entities;

namespace edu_connect_backend.Application.Mappers
{
    public class EventoMapper
    {
        public EventoMapper()
        {
        }

        public EventoResponseDTO ToEventoResponseDTO(Evento model)
        {
            return new EventoResponseDTO
            {
                id = model.id,
                title = model.titulo,
                start = model.dataInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = model.dataFim?.ToString("yyyy-MM-ddTHH:mm:ss"),
                type = model.tipo,
                description = model.descricao,
                turmaNome = model.turma != null ? model.turma.nome : "Geral"
            };
        }

        public List<EventoResponseDTO> ToEventoResponseDTOList(List<Evento> models)
        {
            return models.Select(ToEventoResponseDTO).ToList();
        }

        public Evento ToEvento(EventoRequestDTO dto)
        {
            return new Evento
            {
                titulo = dto.titulo,
                descricao = dto.descricao,
                dataInicio = dto.dataInicio,
                dataFim = dto.dataFim,
                tipo = dto.tipo,
                turmaId = dto.turmaId
            };
        }
    }
}