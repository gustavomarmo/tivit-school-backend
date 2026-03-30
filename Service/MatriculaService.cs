using edu_connect_backend.Configuration;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using Microsoft.Extensions.Options;

namespace edu_connect_backend.Service
{
    public class MatriculaService
    {
        private readonly MatriculaRepository matriculaRepository;
        private readonly EduConnectVariables config;

        public MatriculaService(MatriculaRepository matriculaRepository, IOptions<EduConnectVariables> config)
        {
            this.matriculaRepository = matriculaRepository;
            this.config = config.Value;
        }

        public async Task<SolicitacaoMatricula?> ObterPorEmailOuCpf(string email, string cpf)
        {
            return await matriculaRepository.ObterPorEmailOuCpf(email, cpf);
        }

        public async Task<SolicitacaoMatricula?> ObterPorEmailEOtp(string email, string otp)
        {
            return await matriculaRepository.ObterPorEmailEOtp(email, otp);
        }

        public async Task<SolicitacaoMatricula?> ObterPorId(int id)
        {
            return await matriculaRepository.ObterPorId(id);
        }

        public async Task<SolicitacaoMatricula?> ObterPorIdComDocumentos(int id)
        {
            return await matriculaRepository.ObterPorIdComDocumentos(id);
        }

        public async Task<List<SolicitacaoMatricula>> ListarPendentes()
        {
            return await matriculaRepository.ListarPendentes();
        }

        public async Task<List<ConfiguracaoVaga>> ObterVagasDoAnoAtual()
        {
            return await matriculaRepository.ObterVagasDoAnoAtual();
        }

        public async Task<int> ContarOcupacoes(string serie, Turno turno, int? ignorarId = null)
        {
            return await matriculaRepository.ContarOcupacoes(serie, turno, ignorarId);
        }

        public async Task IniciarOuAtualizarSolicitacao(SolicitacaoMatricula existente, SolicitacaoMatricula dados, string otp)
        {
            existente.nomeCompleto = dados.nomeCompleto;
            existente.email = dados.email;
            existente.telefone = dados.telefone;
            existente.dataNascimento = dados.dataNascimento;
            existente.codigoOtp = otp;
            existente.validadeOtp = DateTime.Now.AddMinutes(30);

            await matriculaRepository.Atualizar(existente);
        }

        public async Task CriarSolicitacao(SolicitacaoMatricula solicitacao, string otp)
        {
            solicitacao.codigoOtp = otp;
            solicitacao.validadeOtp = DateTime.Now.AddMinutes(30);

            await matriculaRepository.Criar(solicitacao);
        }

        public async Task AtualizarStatus(SolicitacaoMatricula solicitacao, StatusMatricula novoStatus)
        {
            solicitacao.status = novoStatus;
            await matriculaRepository.Atualizar(solicitacao);
        }

        public async Task SalvarDadosComplementares(SolicitacaoMatricula solicitacao, SolicitacaoMatricula dados)
        {
            solicitacao.enderecoCompleto = dados.enderecoCompleto;
            solicitacao.rg = dados.rg;
            solicitacao.nomeResponsavel = dados.nomeResponsavel;
            solicitacao.contatoResponsavel = dados.contatoResponsavel;
            solicitacao.escolaridadeAnterior = dados.escolaridadeAnterior;

            await matriculaRepository.Atualizar(solicitacao);
        }

        public async Task<DocumentoMatricula> SalvarDocumento(DocumentoMatricula documento)
        {
            return await matriculaRepository.SalvarDocumento(documento);
        }

        public async Task SelecionarVaga(SolicitacaoMatricula solicitacao, SolicitacaoMatricula dados, decimal valorMensalidade)
        {
            solicitacao.serieDesejada = dados.serieDesejada;
            solicitacao.turnoDesejado = dados.turnoDesejado;
            solicitacao.valorMensalidade = valorMensalidade;

            await matriculaRepository.Atualizar(solicitacao);
        }

        public async Task FinalizarMatricula(SolicitacaoMatricula solicitacao)
        {
            string anoAtual = DateTime.Now.Year.ToString();
            int totalAlunos = await matriculaRepository.ContarAlunos();
            string matricula = $"{anoAtual}{(totalAlunos + 1):D4}";

            var novoUsuario = new Usuario
            {
                nome = solicitacao.nomeCompleto,
                email = matricula + (config.DOMINIO_EMAIL_ALUNO ?? "@aluno.educonnect.com"),
                senhaHash = BCrypt.Net.BCrypt.HashPassword(
                    solicitacao.cpf.Replace(".", "").Replace("-", "")[..6]),
                cpf = solicitacao.cpf,
                perfil = PerfilUsuario.Aluno,
                ativo = true,
                dataCadastro = DateTime.Now
            };

            var novoAluno = new Aluno
            {
                matricula = matricula,
                dataNascimento = solicitacao.dataNascimento,
                turmaId = null
            };

            await matriculaRepository.CriarUsuarioEAluno(novoUsuario, novoAluno);

            solicitacao.status = StatusMatricula.Finalizado;
            await matriculaRepository.Atualizar(solicitacao);
        }
    }
}