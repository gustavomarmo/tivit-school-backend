using edu_connect_backend.Configuration;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using Microsoft.Extensions.Options;
using System.Data;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace edu_connect_backend.Service
{
    public class ProfessorService
    {
        private readonly EduConnectVariables config;
        private readonly ProfessorRepository professorRepository;
        private readonly UsuarioRepository usuarioRepository;

        public ProfessorService(IOptions<EduConnectVariables> config, ProfessorRepository professorRepository, UsuarioRepository usuarioRepository)
        {
            this.config = config.Value;
            this.professorRepository = professorRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public List<Professor> ListarProfessores(string? busca)
        {
            return professorRepository.Listar(busca);
        }

        public void CriarProfessor(Professor novoProfessor)
        {
            if (novoProfessor.usuario == null)
                novoProfessor.usuario = new Usuario();

            novoProfessor.usuario.email = $"{novoProfessor.matricula}@professor.educonnect.com";

            if (usuarioRepository.ObterUsuarioPorEmail(novoProfessor.usuario.email) != null)
                throw new Exception("Já existe um usuário com este e-mail/matrícula.");

            novoProfessor.usuario.senhaHash = BCrypt.Net.BCrypt.HashPassword(config.SENHA_PADRAO);
            novoProfessor.usuario.cpf = "";
            novoProfessor.usuario.perfil = PerfilUsuario.Professor;
            novoProfessor.usuario.dataCadastro = DateTime.Now;

            professorRepository.Criar(novoProfessor);
        }

        public bool? EditarProfessor(int id, Professor dadosAtualizados)
        {
            var professorBanco = professorRepository.ObterPorId(id)
                ?? throw new KeyNotFoundException("Professor não encontrado");

            professorBanco.matricula = dadosAtualizados.matricula;
            professorBanco.especialidade = dadosAtualizados.especialidade;

            if (professorBanco.usuario != null && dadosAtualizados.usuario != null)
            {
                professorBanco.usuario.nome = dadosAtualizados.usuario.nome;
                professorBanco.usuario.ativo = dadosAtualizados.usuario.ativo;
            }

            professorRepository.Atualizar(professorBanco);
            return true;
        }

        public bool DeletarProfessor(int id)
        {
            var professor = professorRepository.ObterPorId(id)
                ?? throw new KeyNotFoundException("Professor não encontrado");

            professorRepository.Deletar(professor);
            return true;
        }
    }
}