using edu_connect_backend.Context;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace edu_connect_backend.Service
{
    public class AtividadeService
    {
        private readonly MaterialRepository materialRepository;
        private readonly AtividadeRepository atividadeRepository;
        private readonly ConnectionContext context;
        private readonly IWebHostEnvironment env;
        public AtividadeService(
            MaterialRepository materialRepository,
            AtividadeRepository atividadeRepository,
            ConnectionContext context,
            IWebHostEnvironment env)
        {
            this.materialRepository = materialRepository;
            this.atividadeRepository = atividadeRepository;
            this.context = context;
            this.env = env;
        }

        public void CriarAtividade(Material atividade)
        {
            materialRepository.AdicionarMaterial(atividade);
        }

        public void EditarAtividade(int id, Material dadosAtualizados)
        {
            var material = materialRepository.ObterMaterialPorId(id);
            if (material == null) throw new Exception("Atividade não encontrada.");
            if (material.tipo != "assignment") throw new Exception("Este item não é uma atividade.");

            material.titulo = dadosAtualizados.titulo;
            material.descricao = dadosAtualizados.descricao;
            material.dataEntrega = dadosAtualizados.dataEntrega;
            material.notaMaxima = dadosAtualizados.notaMaxima;

            materialRepository.AtualizarMaterial(material);
        }

        public void DeletarAtividade(int id)
        {
            var material = materialRepository.ObterMaterialPorId(id);
            if (material != null)
                materialRepository.DeletarMaterial(material);  
        }

        public string RegistrarEntrega(int atividadeId, int usuarioId, IFormFile arquivo)
        {
            var aluno = context.alunos.FirstOrDefault(a => a.usuarioId == usuarioId);
            if (aluno == null) throw new Exception("Perfil de aluno não encontrado para este usuário.");

            var material = materialRepository.ObterMaterialPorId(atividadeId);
            if (material == null) throw new Exception("Atividade não encontrada.");

            if (material.tipo != "assignment" && material.dataEntrega == null)
                throw new Exception("Este material não é uma atividade.");

            if (arquivo == null || arquivo.Length == 0) throw new Exception("Arquivo inválido.");

            string pastaUploads = Path.Combine(env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads");

            if (!Directory.Exists(pastaUploads))
                Directory.CreateDirectory(pastaUploads);

            string nomeArquivo = $"{DateTime.Now.Ticks}_{arquivo.FileName}";
            string caminhoCompleto = Path.Combine(pastaUploads, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                arquivo.CopyTo(stream);
            }

            var novaEntrega = new Entrega
            {
                materialId = atividadeId,
                alunoId = aluno.id,
                arquivoUrl = $"/uploads/{nomeArquivo}",
                dataEntrega = DateTime.Now
            };

            atividadeRepository.AdicionarEntrega(novaEntrega);

            return novaEntrega.arquivoUrl;
        }
    }
}