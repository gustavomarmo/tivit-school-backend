using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Domain.Entities;

namespace edu_connect_backend.Application.Mappers
{
    public class AtividadeMapper
    {
        public AtividadeMapper()
        {
        }

        public Material ToMaterial(AtividadeRequestDTO dto)
        {
            return new Material
            {
                titulo = dto.titulo,
                descricao = dto.descricao,
                tipo = "assignment",
                url = "",
                topicoId = dto.topicoId,
                dataEntrega = dto.dataEntrega,
                notaMaxima = dto.notaMaxima
            };
        }
    }
}