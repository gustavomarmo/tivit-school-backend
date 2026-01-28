using edu_connect_backend.DTO;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class FrequenciaMapper
    {
        public FrequenciaMapper()
        {
        }

        public List<Frequencia> ToFrequenciaList(ChamadaRequestDTO dto)
        {
            var lista = new List<Frequencia>();

            if (dto.registros != null)
            {
                foreach (var registro in dto.registros)
                {
                    lista.Add(new Frequencia
                    {
                        dataAula = dto.data,
                        disciplinaId = dto.disciplinaId,
                        alunoId = registro.alunoId,
                        presente = registro.presente
                    });
                }
            }

            return lista;
        }

        public FrequenciaResumoDTO ToFrequenciaResumoDTO(FrequenciaResumoReadModel model)
        {
            return new FrequenciaResumoDTO
            {
                disciplina = model.disciplina,
                totalAulas = model.totalAulas,
                faltas = model.totalFaltas,
                frequencia = model.frequencia
            };
        }

        public List<FrequenciaResumoDTO> ToFrequenciaResumoDTOList(List<FrequenciaResumoReadModel> models)
        {
            return models.Select(ToFrequenciaResumoDTO).ToList();
        }
    }
}