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

        public List<AlunoResponseDTO> listarAlunos(string? busca)
        {
            var alunos = repository.obterAlunos(busca);

            return alunos.Select(a => new AlunoResponseDTO
            {
                id = a.id,
                nome = a.usuario.nome,
                email = a.usuario.email,
                matricula = a.matricula,
                turma = a.turma?.nome ?? "Sem Turma"
            }).ToList();
        }

        public void criarAluno(AlunoRequestDTO dto)
        {
            string emailGerado = $"{dto.matricula}@aluno.educonnect.com";

            var novoUsuario = new Usuario
            {
                nome = dto.nome,
                email = emailGerado,
                senhaHash = "Mudar123!",
                cpf = "",
                perfil = PerfilUsuario.Aluno,
                ativo = dto.ativo,
                dataCadastro = DateTime.Now
            };

            var novoAluno = new Aluno
            {
                matricula = dto.matricula,
                dataNascimento = DateTime.Now,
                turmaId = dto.turmaId,
                usuario = novoUsuario
            };

            repository.Criar(novoAluno);
        }

        public bool? editarAluno(int id, AlunoRequestDTO dto)
        {
            var aluno = repository.ObterPorId(id);
            if (aluno == null) return null;

            aluno.usuario.nome = dto.nome;
            aluno.matricula = dto.matricula;
            aluno.turmaId = dto.turmaId;
            aluno.usuario.ativo = dto.ativo;

            repository.Atualizar(aluno);
            return true;
        }

        public bool deletarAluno(int id)
        {
            var aluno = repository.ObterPorId(id);
            if (aluno == null) return false;

            repository.Deletar(aluno);
            return true;
        }
    }
}