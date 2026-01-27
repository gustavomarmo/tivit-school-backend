using edu_connect_backend.DTO;
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

        public void CriarMaterial(MaterialRequestDTO dto)
        {
            var material = new Material
            {
                titulo = dto.titulo,
                tipo = dto.tipo,
                url = dto.url,
                topicoId = dto.topicoId,
                descricao = ""
            };
            this.materialRepository.AdicionarMaterial(material);
        }

        public void EditarMaterial(int id, MaterialRequestDTO dto)
        {
            var material = this.materialRepository.ObterMaterialPorId(id);
            if (material == null) throw new Exception("Material não encontrado.");

            material.titulo = dto.titulo;
            material.url = dto.url;
            material.tipo = dto.tipo;
            material.topicoId = dto.topicoId;

            this.materialRepository.AtualizarMaterial(material);
        }

        public void DeletarMaterial(int id)
        {
            var material = this.materialRepository.ObterMaterialPorId(id);
            if (material == null) throw new Exception("Material não encontrado.");

            this.materialRepository.DeletarMaterial(material);
        }
    }
}