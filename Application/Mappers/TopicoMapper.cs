using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Domain.Entities;

namespace edu_connect_backend.Application.Mappers
{
    public class TopicoMapper
    {

        public Topico ToTopico(TopicoRequestDTO dto)
        {
            return new Topico
            {
                titulo = dto.titulo,
                turmaDisciplinaId = dto.turmaDisciplinaId
            };
        }
    }
}