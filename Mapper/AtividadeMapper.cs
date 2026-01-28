using edu_connect_backend.DTO;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
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