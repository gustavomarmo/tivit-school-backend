using edu_connect_backend.Context;
using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace edu_connect_backend.Service
{
    public class AtividadeService
    {
        private readonly MaterialRepository materialRepository;
        private readonly AtividadeRepository atividadeRepository;
        private readonly ConnectionContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment env;

        public AtividadeService(
            MaterialRepository materialRepository,
            AtividadeRepository atividadeRepository,
            ConnectionContext context,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env)
        {
            this.materialRepository = materialRepository;
            this.atividadeRepository = atividadeRepository;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.env = env;
        }

        public void CriarAtividade(AtividadeRequestDTO dto)
        {
            var atividade = new Material
            {
                titulo = dto.titulo,
                descricao = dto.descricao,
                tipo = "assignment",
                url = "",
                topicoId = dto.topicoId,
                dataEntrega = dto.dataEntrega,
                notaMaxima = dto.notaMaxima
            };
            this.materialRepository.AdicionarMaterial(atividade);
        }

        public void EditarAtividade(int id, AtividadeRequestDTO dto)
        {
            var material = this.materialRepository.ObterMaterialPorId(id);
            if (material == null) throw new Exception("Atividade não encontrada.");
            if (material.tipo != "assignment") throw new Exception("Este item não é uma atividade.");

            material.titulo = dto.titulo;
            material.descricao = dto.descricao;
            material.dataEntrega = dto.dataEntrega;
            material.notaMaxima = dto.notaMaxima;

            this.materialRepository.AtualizarMaterial(material);
        }

        public void DeletarAtividade(int id)
        {
            var material = this.materialRepository.ObterMaterialPorId(id);
            if (material != null)
                this.materialRepository.DeletarMaterial(material);
        }

        public string RegistrarEntrega(int atividadeId, IFormFile arquivo)
        {
            var usuarioId = ObterIdUsuarioLogado();

            var aluno = this.context.alunos.FirstOrDefault(a => a.usuarioId == usuarioId);
            if (aluno == null) throw new Exception("Perfil de aluno não encontrado para este usuário.");

            var material = this.materialRepository.ObterMaterialPorId(atividadeId);
            if (material == null) throw new Exception("Atividade não encontrada.");

            if (material.tipo != "assignment" && material.dataEntrega == null)
                throw new Exception("Este material não é uma atividade.");

            if (arquivo == null || arquivo.Length == 0) throw new Exception("Arquivo inválido.");

            string pastaUploads = Path.Combine(this.env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads");

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

            this.atividadeRepository.AdicionarEntrega(novaEntrega);

            return novaEntrega.arquivoUrl;
        }

        private int ObterIdUsuarioLogado()
        {
            var idClaim = this.httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id)) return id;
            throw new Exception("Usuário não identificado.");
        }
    }
}