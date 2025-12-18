using edu_connect_backend.Context;
using edu_connect_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class AlunoRepository
    {
        private readonly ConnectionContext context;

        public AlunoRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public List<Aluno> ObterAlunos(string? busca)
        {
            var query = context.alunos
                .Include(a => a.usuario)
                .Include(a => a.turma)
                .AsQueryable();

            if (!string.IsNullOrEmpty(busca))
            {
                query = query.Where(a => a.usuario.nome.Contains(busca) || a.matricula.Contains(busca));
            }

            return query.ToList();
        }

        public Aluno? ObterPorId(int id)
        {
            return context.alunos
                .Include(a => a.usuario)
                .Include(a => a.turma)
                .FirstOrDefault(a => a.id == id);
        }

        public bool UsuarioJaEAluno(int usuarioId)
        {
            return context.alunos.Any(a => a.usuarioId == usuarioId);
        }

        public void Adicionar(Aluno aluno)
        {
            context.alunos.Add(aluno);
            context.SaveChanges();
        }

        public void Atualizar(Aluno aluno)
        {
            context.alunos.Update(aluno);
            context.SaveChanges();
        }

        public void Deletar(Aluno aluno)
        {
            if (aluno.usuario != null)
            {
                aluno.usuario.ativo = false;
                context.usuarios.Update(aluno.usuario);
            }

            context.alunos.Remove(aluno);
            context.SaveChanges();
        }
    }
}