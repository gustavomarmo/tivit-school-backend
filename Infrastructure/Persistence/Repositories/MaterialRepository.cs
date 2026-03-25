using edu_connect_backend.Domain.Entities;
using edu_connect_backend.Infrastructure.Persistence.Context;

namespace edu_connect_backend.Infrastructure.Persistence.Repositories
{
    public class MaterialRepository
    {
        private readonly ConnectionContext context;

        public MaterialRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public Material? ObterMaterialPorId(int id)
        {
            return this.context.Materiais.FirstOrDefault(m => m.id == id);
        }

        public void AdicionarMaterial(Material material)
        {
            this.context.Materiais.Add(material);
            this.context.SaveChanges();
        }

        public void AtualizarMaterial(Material material)
        {
            this.context.Materiais.Update(material);
            this.context.SaveChanges();
        }

        public void DeletarMaterial(Material material)
        {
            this.context.Materiais.Remove(material);
            this.context.SaveChanges();
        }
    }
}