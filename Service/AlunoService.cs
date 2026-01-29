using edu_connect_backend.Configuration;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using Microsoft.Extensions.Options;

namespace edu_connect_backend.Service
{
    public class AlunoService
    {
        private readonly EduConnectVariables config;
        private readonly AlunoRepository alunoRepository;

        public AlunoService(IOptions<EduConnectVariables> config, AlunoRepository repository)
        {
            this.config = config.Value;
            this.alunoRepository = repository;
        }

        public List<Aluno> ListarAlunos(string? busca)
        {
            return alunoRepository.obterAlunos(busca)
                ?? throw new KeyNotFoundException("Alunos não encontrados");
        }

        public void CriarAluno(Aluno novoAluno)
        {
            if (novoAluno.usuario == null)
                novoAluno.usuario = new Usuario();

            novoAluno.usuario.email = novoAluno.matricula + config.DOMINIO_EMAIL_ALUNO;
            novoAluno.usuario.senhaHash = config.SENHA_PADRAO;
            novoAluno.usuario.cpf = "";
            novoAluno.usuario.perfil = PerfilUsuario.Aluno;
            novoAluno.usuario.dataCadastro = DateTime.Now;

            novoAluno.dataNascimento = DateTime.Now;

            alunoRepository.Criar(novoAluno);
        }

        public bool? EditarAluno(int id, Aluno dadosAtualizados)
        {
            var alunoBanco = alunoRepository.ObterPorId(id)
                ?? throw new KeyNotFoundException("Aluno não encontrado");

            alunoBanco.usuario.nome = dadosAtualizados.usuario.nome;
            alunoBanco.matricula = dadosAtualizados.matricula;
            alunoBanco.turmaId = dadosAtualizados.turmaId;
            alunoBanco.usuario.ativo = dadosAtualizados.usuario.ativo;

            alunoRepository.Atualizar(alunoBanco);
            return true;
        }

        public bool DeletarAluno(int id)
        {
            var aluno = alunoRepository.ObterPorId(id)
            ?? throw new KeyNotFoundException("Aluno não encontrado");

            alunoRepository.Deletar(aluno);
            return true;
        }
    }
}