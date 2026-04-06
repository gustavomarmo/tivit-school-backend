using edu_connect_backend.DTO.Evento;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
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
                Id = model.id,
                Titulo = model.titulo,
                Inicio = model.dataInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                Fim = model.dataFim?.ToString("yyyy-MM-ddTHH:mm:ss"),
                Tipo = model.tipo,
                Descricao = model.descricao,
                TurmaNome = model.turma != null ? model.turma.nome : "Geral"
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
                titulo = dto.Titulo,
                descricao = dto.Descricao,
                dataInicio = dto.DataInicio,
                dataFim = dto.DataFim,
                tipo = dto.Tipo,
                turmaId = dto.TurmaId
            };
        }
    }
}