using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Domain.Entities;

namespace edu_connect_backend.Application.Mappers
{
    public class MaterialMapper
    {
        public Material ToMaterial(MaterialRequestDTO dto)
        {
            return new Material
            {
                titulo = dto.titulo,
                tipo = dto.tipo,
                url = dto.url,
                topicoId = dto.topicoId,
                descricao = ""
            };
        }
    }
}