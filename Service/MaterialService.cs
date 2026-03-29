using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class MaterialService
    {
        private readonly MaterialRepository materialRepository;

        public MaterialService(MaterialRepository materialRepository)
        {
            this.materialRepository = materialRepository;
        }

        public void CriarMaterial(Material material)
        {
            materialRepository.AdicionarMaterial(material);
        }

        public void EditarMaterial(int id, Material dadosAtualizados)
        {
            var material = materialRepository.ObterMaterialPorId(id)
                ?? throw new KeyNotFoundException("Material não encontrado.");

            material.titulo = dadosAtualizados.titulo;
            material.url = dadosAtualizados.url;
            material.tipo = dadosAtualizados.tipo;
            material.topicoId = dadosAtualizados.topicoId;

            materialRepository.AtualizarMaterial(material);
        }

        public void DeletarMaterial(int id)
        {
            var material = materialRepository.ObterMaterialPorId(id)
                ?? throw new KeyNotFoundException("Material não encontrado.");

            materialRepository.DeletarMaterial(material);
        }
    }
}