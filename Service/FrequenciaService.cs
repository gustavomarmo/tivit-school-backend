using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class FrequenciaService
    {
        private readonly FrequenciaRepository repository;

        public FrequenciaService(FrequenciaRepository repository)
        {
            this.repository = repository;
        }

        public void RealizarChamada(ChamadaRequestDTO dto)
        {
            var listaParaSalvar = new List<Frequencia>();

            foreach (var registro in dto.registros)
            {
                listaParaSalvar.Add(new Frequencia
                {
                    dataAula = dto.data,
                    disciplinaId = dto.disciplinaId,
                    alunoId = registro.alunoId,
                    presente = registro.presente
                });
            }

            repository.Registrar(listaParaSalvar);
        }
    }
}