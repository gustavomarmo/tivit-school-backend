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
        private readonly AlunoRepository alunoRepository;
        private readonly IWebHostEnvironment env;
        public AtividadeService(
            MaterialRepository materialRepository,
            AtividadeRepository atividadeRepository,
            AlunoRepository alunoRepository,
            IWebHostEnvironment env)
        {
            this.materialRepository = materialRepository;
            this.atividadeRepository = atividadeRepository;
            this.alunoRepository = alunoRepository;
            this.env = env;
        }

        public void CriarAtividade(Material atividade)
        {
            materialRepository.AdicionarMaterial(atividade);
        }

        public void EditarAtividade(int id, Material dadosAtualizados)
        {
            var material = materialRepository.ObterMaterialPorId(id)
                ?? throw new KeyNotFoundException("Material não encontrado");

            if (material.tipo != "assignment") throw new Exception("Este item não é uma atividade.");

            material.titulo = dadosAtualizados.titulo;
            material.descricao = dadosAtualizados.descricao;
            material.dataEntrega = dadosAtualizados.dataEntrega;
            material.notaMaxima = dadosAtualizados.notaMaxima;

            materialRepository.AtualizarMaterial(material);
        }

        public void DeletarAtividade(int id)
        {
            var material = materialRepository.ObterMaterialPorId(id)
                ?? throw new KeyNotFoundException("Material não encontrado");

            materialRepository.DeletarMaterial(material);  
        }

        public string RegistrarEntrega(int atividadeId, int usuarioId, IFormFile arquivo)
        {
            var aluno = alunoRepository.ObterAlunoPorUsuarioId(usuarioId)
                ?? throw new KeyNotFoundException("Perfil de aluno não encontrado para este usuário.");

            var material = materialRepository.ObterMaterialPorId(atividadeId)
                 ?? throw new KeyNotFoundException("Material não encontrado.");

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