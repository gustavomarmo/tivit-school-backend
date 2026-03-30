using edu_connect_backend.DTO;
using edu_connect_backend.Mapper;
using edu_connect_backend.Model;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/matriculas")]
    public class MatriculaController : ControllerBase
    {
        private readonly MatriculaService matriculaService;
        private readonly MatriculaMapper matriculaMapper;
        private readonly EmailService emailService;

        public MatriculaController(
            MatriculaService matriculaService,
            MatriculaMapper matriculaMapper,
            EmailService emailService)
        {
            this.matriculaService = matriculaService;
            this.matriculaMapper = matriculaMapper;
            this.emailService = emailService;
        }

        [HttpPost("iniciar")]
        public async Task<IActionResult> IniciarMatricula([FromBody] MatriculaInicialDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var solicitacaoExistente = await matriculaService.ObterPorEmailOuCpf(dto.Email, dto.Cpf);

            if (solicitacaoExistente?.status == StatusMatricula.Finalizado)
                return BadRequest(new { mensagem = "Este CPF/E-mail já consta como matriculado. Faça login na área do aluno." });

            string otp = GerarOtp();
            bool ehReenvio = solicitacaoExistente != null;

            if (ehReenvio)
                await matriculaService.IniciarOuAtualizarSolicitacao(
                    solicitacaoExistente!, matriculaMapper.ToSolicitacaoMatricula(dto), otp);
            else
                await matriculaService.CriarSolicitacao(matriculaMapper.ToSolicitacaoMatricula(dto), otp);

            try
            {
                string assunto = ehReenvio
                    ? "Edu Connect - Novo Código de Verificação"
                    : "Edu Connect - Código de Verificação";

                string corpo = $"Olá, {dto.NomeCompleto}!\n\n" +
                    (ehReenvio
                        ? "Você solicitou um novo código de acesso para matrícula no Edu Connect."
                        : "Recebemos sua solicitação de matrícula.") +
                    $"\n\nSeu código de verificação é: {otp}\n\nValidade: 30 minutos.";

                await emailService.SendEmailAsync(dto.Email, dto.NomeCompleto, assunto, corpo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Solicitação salva, mas erro ao enviar e-mail: {ex.Message}" });
            }

            var solicitacao = ehReenvio
                ? solicitacaoExistente!
                : await matriculaService.ObterPorEmailOuCpf(dto.Email, dto.Cpf);

            return Ok(new
            {
                mensagem = ehReenvio
                    ? "Identificamos um cadastro em andamento. Um NOVO código foi enviado para seu e-mail."
                    : "Solicitação iniciada! O código foi enviado para seu e-mail.",
                idSolicitacao = solicitacao!.id,
                reenvio = ehReenvio
            });
        }

        [HttpPost("validar-otp")]
        public async Task<IActionResult> ValidarOtp([FromBody] ValidarOtpDTO dto)
        {
            var solicitacao = await matriculaService.ObterPorEmailEOtp(dto.Email, dto.Codigo);

            if (solicitacao == null)
                return BadRequest(new { mensagem = "E-mail ou código inválidos." });

            if (solicitacao.validadeOtp < DateTime.Now)
                return BadRequest(new { mensagem = "O código expirou. Solicite um novo." });

            if (solicitacao.status == StatusMatricula.Iniciado)
                await matriculaService.AtualizarStatus(solicitacao, StatusMatricula.AguardandoDados);

            return Ok(matriculaMapper.ToMatriculaResponseDTO(solicitacao));
        }

        [HttpPut("dados-complementares")]
        public async Task<IActionResult> SalvarDadosComplementares([FromBody] MatriculaPasso2DTO dto)
        {
            var solicitacao = await matriculaService.ObterPorId(dto.SolicitacaoId)
                ?? throw new KeyNotFoundException("Solicitação não encontrada.");

            await matriculaService.SalvarDadosComplementares(
                solicitacao, matriculaMapper.ToDadosComplementares(dto));

            return Ok(new { mensagem = "Dados salvos com sucesso!" });
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocumento(
            [FromForm] int solicitacaoId,
            [FromForm] TipoDocumentoMatricula tipo,
            IFormFile arquivo,
            [FromServices] BlobService blobService)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest(new { mensagem = "Arquivo inválido." });

            var solicitacao = await matriculaService.ObterPorId(solicitacaoId)
                ?? throw new KeyNotFoundException("Solicitação não encontrada.");

            var url = await blobService.UploadAsync(arquivo, $"matriculas/{solicitacaoId}");
            var documento = matriculaMapper.ToDocumentoMatricula(solicitacaoId, tipo, url, arquivo.FileName);
            var documentoSalvo = await matriculaService.SalvarDocumento(documento);

            return Ok(new { mensagem = "Upload realizado com sucesso!", idDocumento = documentoSalvo.id });
        }

        [HttpGet("vagas-disponiveis")]
        public async Task<IActionResult> ObterVagas()
        {
            var configuracoes = await matriculaService.ObterVagasDoAnoAtual();

            if (!configuracoes.Any())
                return NotFound(new { mensagem = "Nenhuma vaga configurada para o ano letivo atual." });

            var resultado = new List<object>();

            foreach (var config in configuracoes)
            {
                int ocupadas = await matriculaService.ContarOcupacoes(config.serie, config.turno);
                int vagasRestantes = config.vagasTotais - ocupadas;

                resultado.Add(new
                {
                    serie = config.serie,
                    turno = config.turno.ToString(),
                    valor = config.valorMensalidade,
                    vagasRestantes,
                    disponivel = vagasRestantes > 0
                });
            }

            return Ok(resultado);
        }

        [HttpPut("selecionar-vaga")]
        public async Task<IActionResult> SelecionarVaga([FromBody] SelecaoVagaDTO dto)
        {
            var solicitacao = await matriculaService.ObterPorId(dto.SolicitacaoId)
                ?? throw new KeyNotFoundException("Solicitação não encontrada.");

            var dadosVaga = matriculaMapper.ToSelecaoVaga(dto);

            var configuracoes = await matriculaService.ObterVagasDoAnoAtual();
            var config = configuracoes.FirstOrDefault(
                c => c.serie == dadosVaga.serieDesejada && c.turno == dadosVaga.turnoDesejado);

            if (config == null)
                return NotFound(new { mensagem = "Configuração de vaga não encontrada." });

            int ocupadas = await matriculaService.ContarOcupacoes(
                dadosVaga.serieDesejada!, dadosVaga.turnoDesejado!.Value, dto.SolicitacaoId);

            if (ocupadas >= config.vagasTotais)
                return BadRequest(new { mensagem = "Não há vagas disponíveis para esta série/turno." });

            await matriculaService.SelecionarVaga(solicitacao, dadosVaga, config.valorMensalidade);

            return Ok(new { mensagem = "Vaga selecionada com sucesso!", valor = config.valorMensalidade });
        }

        [HttpPut("aceitar-termos")]
        public async Task<IActionResult> AceitarTermos([FromBody] AceitarTermosDTO dto)
        {
            if (!dto.TermosAceitos)
                return BadRequest(new { mensagem = "Os termos precisam ser aceitos para continuar." });

            var solicitacao = await matriculaService.ObterPorId(dto.SolicitacaoId)
                ?? throw new KeyNotFoundException("Solicitação não encontrada.");

            await matriculaService.AtualizarStatus(solicitacao, StatusMatricula.AguardandoAnaliseDocs);

            return Ok(new { mensagem = "Termos aceitos. Aguardando análise da coordenação." });
        }

        [HttpPost("comprovante-pix")]
        public async Task<IActionResult> UploadComprovantePix(
            [FromForm] int solicitacaoId,
            IFormFile arquivo,
            [FromServices] BlobService blobService)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest(new { mensagem = "Arquivo inválido." });

            var solicitacao = await matriculaService.ObterPorId(solicitacaoId)
                ?? throw new KeyNotFoundException("Solicitação não encontrada.");

            if (solicitacao.status != StatusMatricula.AguardandoPagamento)
                return BadRequest(new { mensagem = "Solicitação não está aguardando pagamento." });

            var url = await blobService.UploadAsync(arquivo, $"matriculas/{solicitacaoId}");
            var documento = matriculaMapper.ToDocumentoMatricula(
                solicitacaoId, TipoDocumentoMatricula.ComprovantePagamento, url, arquivo.FileName);

            await matriculaService.SalvarDocumento(documento);
            await matriculaService.AtualizarStatus(solicitacao, StatusMatricula.AguardandoAnaliseDocs);

            return Ok(new { mensagem = "Comprovante enviado! Aguardando confirmação do financeiro." });
        }

        [HttpGet("pendentes")]
        [Authorize(Roles = "Coordenador,Admin")]
        public async Task<IActionResult> ListarPendentes()
        {
            var pendentes = await matriculaService.ListarPendentes();
            return Ok(matriculaMapper.ToMatriculaPendenteResponseDTOList(pendentes));
        }

        [HttpPut("{id}/avaliar")]
        [Authorize(Roles = "Coordenador,Admin")]
        public async Task<IActionResult> AvaliarMatricula(int id, [FromBody] AvaliacaoMatriculaDTO dto)
        {
            var solicitacao = await matriculaService.ObterPorIdComDocumentos(id)
                ?? throw new KeyNotFoundException("Solicitação não encontrada.");

            if (dto.Aprovado)
                return await AprovarMatricula(solicitacao);

            return await RejeitarMatricula(solicitacao, dto.Observacao);
        }

        private async Task<IActionResult> AprovarMatricula(Model.SolicitacaoMatricula solicitacao)
        {
            if (solicitacao.status == StatusMatricula.AguardandoAnaliseDocs)
            {
                await matriculaService.AtualizarStatus(solicitacao, StatusMatricula.AguardandoPagamento);

                await emailService.SendEmailAsync(
                    solicitacao.email,
                    solicitacao.nomeCompleto,
                    "Edu Connect - Documentação Aprovada!",
                    $"Olá {solicitacao.nomeCompleto}, sua documentação foi aprovada!\n\n" +
                    $"Realize o pagamento da 1ª mensalidade no valor de R$ {solicitacao.valorMensalidade:F2} via PIX.");

                return Ok(new { mensagem = "Documentação aprovada. Candidato notificado para pagamento." });
            }

            if (solicitacao.status == StatusMatricula.AguardandoPagamento)
            {
                await matriculaService.FinalizarMatricula(solicitacao);

                await emailService.SendEmailAsync(
                    solicitacao.email,
                    solicitacao.nomeCompleto,
                    "Edu Connect - Matrícula Concluída!",
                    $"Parabéns, {solicitacao.nomeCompleto}! Sua matrícula foi concluída. Seu acesso ao sistema já está disponível.");

                return Ok(new { mensagem = "Matrícula finalizada. Aluno criado no sistema." });
            }

            return BadRequest(new { mensagem = $"Status '{solicitacao.status}' não permite aprovação." });
        }

        private async Task<IActionResult> RejeitarMatricula(Model.SolicitacaoMatricula solicitacao, string? observacao)
        {
            await matriculaService.AtualizarStatus(solicitacao, StatusMatricula.AguardandoDados);

            await emailService.SendEmailAsync(
                solicitacao.email,
                solicitacao.nomeCompleto,
                "Edu Connect - Documentação Devolvida",
                $"Olá {solicitacao.nomeCompleto},\n\nSua documentação foi devolvida para correção.\n" +
                $"Motivo: {observacao ?? "Documentação inválida ou incompleta."}");

            return Ok(new { mensagem = "Solicitação devolvida ao candidato." });
        }

        private static string GerarOtp() =>
            new Random().Next(100000, 999999).ToString();
    }
}