using edu_connect_backend.DTO.Frequencia;
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

            if (dto.Registros != null)
            {
                foreach (var registro in dto.Registros)
                {
                    lista.Add(new Frequencia
                    {
                        dataAula = dto.Data,
                        disciplinaId = dto.DisciplinaId,
                        alunoId = registro.AlunoId,
                        presente = registro.Presente
                    });
                }
            }

            return lista;
        }

        public FrequenciaResumoResponseDTO ToFrequenciaResumoDTO(FrequenciaResumoReadModel model)
        {
            return new FrequenciaResumoResponseDTO
            {
                Disciplina = model.disciplina,
                TotalAulas = model.totalAulas,
                Faltas = model.totalFaltas,
                Frequencia = model.frequencia
            };
        }

        public List<FrequenciaResumoResponseDTO> ToFrequenciaResumoDTOList(List<FrequenciaResumoReadModel> models)
        {
            return models.Select(ToFrequenciaResumoDTO).ToList();
        }
    }
}