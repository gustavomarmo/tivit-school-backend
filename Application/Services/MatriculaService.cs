using edu_connect_backend.Domain.Entities;
using edu_connect_backend.Domain.Enums;
using edu_connect_backend.Infrastructure.Persistence.Context;
using edu_connect_backend.WebAPI.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace edu_connect_backend.Application.Services
{
    public class MatriculaService
    {
        private readonly ConnectionContext context;
        private readonly EduConnectVariables config;

        public MatriculaService(ConnectionContext context, IOptions<EduConnectVariables> config)
        {
            this.context = context;
            this.config = config.Value;
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

        public async Task IniciarOuAtualizarSolicitacao(SolicitacaoMatricula existente, SolicitacaoMatricula dados, string otp)
        {
            existente.nomeCompleto = dados.nomeCompleto;
            existente.email = dados.email;
            existente.telefone = dados.telefone;
            existente.dataNascimento = dados.dataNascimento;
            existente.codigoOtp = otp;
            existente.validadeOtp = DateTime.Now.AddMinutes(30);
            context.solicitacoesMatricula.Update(existente);
            await context.SaveChangesAsync();
        }

        public async Task CriarSolicitacao(SolicitacaoMatricula solicitacao, string otp)
        {
            solicitacao.codigoOtp = otp;
            solicitacao.validadeOtp = DateTime.Now.AddMinutes(30);
            context.solicitacoesMatricula.Add(solicitacao);
            await context.SaveChangesAsync();
        }

        public async Task AtualizarStatus(SolicitacaoMatricula solicitacao, StatusMatricula novoStatus)
        {
            solicitacao.status = novoStatus;
            await context.SaveChangesAsync();
        }

        public async Task SalvarDadosComplementares(SolicitacaoMatricula solicitacao, SolicitacaoMatricula dados)
        {
            solicitacao.enderecoCompleto = dados.enderecoCompleto;
            solicitacao.rg = dados.rg;
            solicitacao.nomeResponsavel = dados.nomeResponsavel;
            solicitacao.contatoResponsavel = dados.contatoResponsavel;
            solicitacao.escolaridadeAnterior = dados.escolaridadeAnterior;
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

        public async Task SelecionarVaga(SolicitacaoMatricula solicitacao, SolicitacaoMatricula dados, decimal valorMensalidade)
        {
            solicitacao.serieDesejada = dados.serieDesejada;
            solicitacao.turnoDesejado = dados.turnoDesejado;
            solicitacao.valorMensalidade = valorMensalidade;
            await context.SaveChangesAsync();
        }

        public async Task FinalizarMatricula(SolicitacaoMatricula solicitacao)
        {
            string anoAtual = DateTime.Now.Year.ToString();
            int totalAlunos = context.alunos.Count();
            string matricula = $"{anoAtual}{(totalAlunos + 1):D4}";

            var novoUsuario = new Usuario
            {
                nome = solicitacao.nomeCompleto,
                email = matricula + (config.DOMINIO_EMAIL_ALUNO ?? "@aluno.educonnect.com"),
                senhaHash = BCrypt.Net.BCrypt.HashPassword(solicitacao.cpf.Replace(".", "").Replace("-", "").Substring(0, 6)),
                cpf = solicitacao.cpf,
                perfil = PerfilUsuario.Aluno,
                ativo = true,
                dataCadastro = DateTime.Now
            };

            context.usuarios.Add(novoUsuario);
            await context.SaveChangesAsync();

            var novoAluno = new Aluno
            {
                matricula = matricula,
                dataNascimento = solicitacao.dataNascimento,
                usuarioId = novoUsuario.id,
                turmaId = null
            };

            context.alunos.Add(novoAluno);
            solicitacao.status = StatusMatricula.Finalizado;
            await context.SaveChangesAsync();
        }
    }
}