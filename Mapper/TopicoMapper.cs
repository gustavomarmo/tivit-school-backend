using edu_connect_backend.DTO.Disciplina;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class TopicoMapper
    {

        public Topico ToTopico(TopicoRequestDTO dto)
        {
            return new Topico
            {
                titulo = dto.Titulo,
                turmaDisciplinaId = dto.TurmaDisciplinaId
            };
        }
    }
}