using edu_connect_backend.Context;
using edu_connect_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class MatriculaRepository
    {
        private readonly ConnectionContext context;

        public MatriculaRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public async Task<SolicitacaoMatricula?> ObterPorEmailOuCpf(string email, string cpf)
        {
            return await context.solicitacoesMatricula
                .FirstOrDefaultAsync(x => x.cpf == cpf || x.email == email);
        }

        public async Task<SolicitacaoMatricula?> ObterPorEmailEOtp(string email, string otp)
        {
            return await context.solicitacoesMatricula
                .FirstOrDefaultAsync(x => x.email == email && x.codigoOtp == otp);
        }

        public async Task<SolicitacaoMatricula?> ObterPorId(int id)
        {
            return await context.solicitacoesMatricula.FindAsync(id);
        }

        public async Task<SolicitacaoMatricula?> ObterPorIdComDocumentos(int id)
        {
            return await context.solicitacoesMatricula
                .Include(s => s.documentos)
                .FirstOrDefaultAsync(s => s.id == id);
        }

        public async Task<List<SolicitacaoMatricula>> ListarPendentes()
        {
            return await context.solicitacoesMatricula
                .Include(s => s.documentos)
                .Where(s =>
                    s.status == StatusMatricula.AguardandoAnaliseDocs ||
                    s.status == StatusMatricula.AguardandoPagamento)
                .OrderByDescending(s => s.dataSolicitacao)
                .ToListAsync();
        }

        public async Task<List<ConfiguracaoVaga>> ObterVagasDoAnoAtual()
        {
            return await context.configuracoesVaga
                .Where(c => c.anoLetivo == DateTime.Now.Year)
                .ToListAsync();
        }

        public async Task<int> ContarOcupacoes(string serie, Turno turno, int? ignorarId = null)
        {
            return await context.solicitacoesMatricula
                .CountAsync(s =>
                    s.serieDesejada == serie &&
                    s.turnoDesejado == turno &&
                    s.status != StatusMatricula.Rejeitado &&
                    s.id != ignorarId);
        }

        public async Task Criar(SolicitacaoMatricula solicitacao)
        {
            context.solicitacoesMatricula.Add(solicitacao);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(SolicitacaoMatricula solicitacao)
        {
            context.solicitacoesMatricula.Update(solicitacao);
            await context.SaveChangesAsync();
        }

        public async Task<DocumentoMatricula> SalvarDocumento(DocumentoMatricula documento)
        {
            var anterior = await context.documentosMatricula
                .FirstOrDefaultAsync(d =>
                    d.solicitacaoMatriculaId == documento.solicitacaoMatriculaId &&
                    d.tipo == documento.tipo);

            if (anterior != null)
                context.documentosMatricula.Remove(anterior);

            context.documentosMatricula.Add(documento);
            await context.SaveChangesAsync();
            return documento;
        }

        public async Task CriarUsuarioEAluno(Usuario usuario, Aluno aluno)
        {
            context.usuarios.Add(usuario);
            await context.SaveChangesAsync();

            aluno.usuarioId = usuario.id;
            context.alunos.Add(aluno);
            await context.SaveChangesAsync();
        }

        public async Task<int> ContarAlunos()
        {
            return await context.alunos.CountAsync();
        }
    }
}