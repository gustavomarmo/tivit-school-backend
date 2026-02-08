using edu_connect_backend.DTO;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
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