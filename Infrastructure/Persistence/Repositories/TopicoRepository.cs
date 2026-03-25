using edu_connect_backend.Domain.Entities;
using edu_connect_backend.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Infrastructure.Persistence.Repositories
{
    public class TopicoRepository
    {
        private readonly ConnectionContext context;

        public TopicoRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public Topico? ObterTopicoPorId(int id)
        {
            return this.context.Topicos.FirstOrDefault(t => t.id == id);
        }

        public void CriarTopico(Topico topico)
        {
            this.context.Topicos.Add(topico);
            this.context.SaveChanges();
        }

        public void AtualizarTopico(Topico topico)
        {
            this.context.Topicos.Update(topico);
            this.context.SaveChanges();
        }

        public void DeletarTopico(Topico topico)
        {
            this.context.Topicos.Remove(topico);
            this.context.SaveChanges();
        }
    }
}