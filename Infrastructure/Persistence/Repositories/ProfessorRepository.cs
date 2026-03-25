using edu_connect_backend.Domain.Entities;
using edu_connect_backend.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Infrastructure.Persistence.Repositories
{
    public class ProfessorRepository
    {
        private readonly ConnectionContext context;

        public ProfessorRepository(ConnectionContext context)
        {
            this.context = context;
        }
        public List<Professor> Listar(string? busca)
        {
            var query = context.professores
                .Include(p => p.usuario)
                .AsQueryable();

            if (!string.IsNullOrEmpty(busca))
            {
                query = query.Where(p => p.usuario.nome.Contains(busca) || p.matricula.Contains(busca));
            }

            return query.ToList();
        }

        public Professor? ObterPorId(int id)
        {
            return context.professores
                .Include(p => p.usuario)
                .FirstOrDefault(p => p.id == id);
        }

        public void Criar(Professor professor)
        {
            context.professores.Add(professor);
            context.SaveChanges();
        }

        public void Atualizar(Professor professor)
        {
            context.professores.Update(professor);
            context.SaveChanges();
        }

        public void Deletar(Professor professor)
        {
            if (professor.usuario != null)
            {
                professor.usuario.ativo = false;
                context.usuarios.Update(professor.usuario);
            }

            context.professores.Remove(professor);
            context.SaveChanges();
        }

        public Professor? ObterProfessorPorUsuarioId(int usuarioId)
        {
            return context.professores.FirstOrDefault(p => p.usuarioId == usuarioId);
        }
    }
}