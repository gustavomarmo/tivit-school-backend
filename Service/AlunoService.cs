using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class AlunoService
    {
        private readonly AlunoRepository repository;

        public AlunoService(AlunoRepository repository)
        {
            this.repository = repository;
        }

        public List<AlunoResponseDTO> ListarAlunos(string? busca)
        {
            var alunos = repository.ObterAlunos(busca);

            return alunos.Select(a => new AlunoResponseDTO
            {
                id = a.id,
                nome = a.usuario.nome,
                email = a.usuario.email,
                matricula = a.matricula,
                turma = a.turma?.nome ?? "Sem Turma" // Trata nulo se não tiver turma
            }).ToList();
        }
    }
}