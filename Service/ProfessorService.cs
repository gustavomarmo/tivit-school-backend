using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class ProfessorService
    {
        private readonly ProfessorRepository repository;

        public ProfessorService(ProfessorRepository repository)
        {
            this.repository = repository;
        }

        public List<ProfessorResponseDTO> listarProfessores(string? busca)
        {
            var professores = repository.ObterProfessores(busca);

            return professores.Select(p => new ProfessorResponseDTO
            {
                id = p.id,
                nome = p.usuario.nome,
                email = p.usuario.email,
                matricula = p.matricula,
                especialidade = p.especialidade
            }).ToList();
        }

        public void criarProfessor(ProfessorRequestDTO dto)
        {
            // Gera e-mail padrão para professor
            string emailGerado = $"{dto.matricula}@professor.educonnect.com";

            var novoUsuario = new Usuario
            {
                nome = dto.nome,
                email = emailGerado,
                senhaHash = "Mudar123!", // Senha Padrão
                cpf = "",
                perfil = PerfilUsuario.Professor, // Enum correto
                ativo = dto.ativo,
                dataCadastro = DateTime.Now
            };

            var novoProfessor = new Professor
            {
                matricula = dto.matricula,
                especialidade = dto.especialidade,
                usuario = novoUsuario
            };

            this.repository.Adicionar(novoProfessor);
        }

        public bool? editarProfessor(int id, ProfessorRequestDTO dto)
        {
            var professor = repository.ObterPorId(id);
            if (professor == null) return null;

            // Atualiza dados do Usuário
            professor.usuario.nome = dto.nome;
            professor.usuario.ativo = dto.ativo;

            // Atualiza dados do Professor
            professor.matricula = dto.matricula;
            professor.especialidade = dto.especialidade;

            repository.Atualizar(professor);
            return true;
        }

        public bool deletarProfessor(int id)
        {
            var professor = repository.ObterPorId(id);
            if (professor == null) return false;

            repository.Deletar(professor);
            return true;
        }
    }
}