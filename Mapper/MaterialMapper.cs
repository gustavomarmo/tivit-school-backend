using edu_connect_backend.DTO.Disciplina;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class MaterialMapper
    {
        public Material ToMaterial(MaterialRequestDTO dto)
        {
            return new Material
            {
                titulo = dto.Titulo,
                tipo = dto.Tipo,
                url = dto.Url,
                topicoId = dto.TopicoId,
                descricao = ""
            };
        }
    }
}