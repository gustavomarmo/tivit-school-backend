using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class FrequenciaService
    {
        private readonly FrequenciaRepository repository;
        private readonly AlunoRepository alunoRepository;

        public FrequenciaService(FrequenciaRepository repository, AlunoRepository alunoRepository)
        {
            this.repository = repository;
            this.alunoRepository = alunoRepository;
        }

        public void RealizarChamada(ChamadaRequestDTO dto)
        {
            var idsAlunos = dto.registros.Select(r => r.alunoId).ToList();

            if (!alunoRepository.TodosAlunosExistem(idsAlunos))
            {
                throw new Exception("Um ou mais alunos informados não existem no banco de dados. Verifique a lista.");
            }

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