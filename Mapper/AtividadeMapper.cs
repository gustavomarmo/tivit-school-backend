using edu_connect_backend.DTO.Disciplina;
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
                titulo = dto.Titulo,
                descricao = dto.Descricao,
                tipo = "assignment",
                url = "",
                topicoId = dto.TopicoId,
                dataEntrega = dto.DataEntrega,
                notaMaxima = dto.NotaMaxima
            };
        }
    }
}