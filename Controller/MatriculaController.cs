using edu_connect_backend.Context;
using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculaController : ControllerBase
    {
        private readonly ConnectionContext _context;
        private readonly EmailService _emailService;

        public MatriculaController(ConnectionContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("iniciar")]
        public async Task<IActionResult> IniciarMatricula([FromBody] MatriculaInicialDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var solicitacao = await _context.solicitacoesMatricula
                .FirstOrDefaultAsync(x => x.cpf == dto.Cpf || x.email == dto.Email);

            string otp = new Random().Next(100000, 999999).ToString();
            string mensagemRetorno = "Solicitação iniciada! O código foi enviado para seu e-mail.";
            bool ehReenvio = false;

            if (solicitacao != null)
            {
                if (solicitacao.status == StatusMatricula.Finalizado)
                {
                    return BadRequest("Este CPF/E-mail já consta como matriculado no sistema. Faça login na área do aluno.");
                }

                solicitacao.nomeCompleto = dto.NomeCompleto;
                solicitacao.email = dto.Email;
                solicitacao.telefone = dto.Telefone;
                solicitacao.dataNascimento = dto.DataNascimento;

                solicitacao.codigoOtp = otp;
                solicitacao.validadeOtp = DateTime.Now.AddMinutes(30);

                _context.solicitacoesMatricula.Update(solicitacao);

                mensagemRetorno = "Identificamos um cadastro em andamento. Um NOVO código foi enviado para seu e-mail.";
                ehReenvio = true;
            }
            else
            {
                solicitacao = new SolicitacaoMatricula
                {
                    nomeCompleto = dto.NomeCompleto,
                    cpf = dto.Cpf,
                    email = dto.Email,
                    telefone = dto.Telefone,
                    dataNascimento = dto.DataNascimento,
                    codigoOtp = otp,
                    validadeOtp = DateTime.Now.AddMinutes(30),
                    status = StatusMatricula.Iniciado,
                    dataSolicitacao = DateTime.Now
                };

                _context.solicitacoesMatricula.Add(solicitacao);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar no banco: {ex.Message}");
            }

            try
            {
                string assunto = ehReenvio ? "Edu Connect - Novo Código de Verificação" : "Edu Connect - Código de Verificação";
                string corpo = $@"
                    Olá, {dto.NomeCompleto}!
                    
                    {(ehReenvio ? "Você solicitou um novo código de acesso para matrícula no Edu Connect." : "Recebemos sua solicitação de matrícula.")}
                    
                    Seu código de verificação é: {otp}
                    
                    Utilize este código para continuar. Validade: 30 minutos.
                ";

                await _emailService.SendEmailAsync(dto.Email, dto.NomeCompleto, assunto, corpo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Solicitação salva, mas erro ao enviar e-mail: {ex.Message}");
            }

            return Ok(new { mensagem = mensagemRetorno, idSolicitacao = solicitacao.id, reenvio = ehReenvio });
        }

        [HttpPost("validar-otp")]
        public async Task<IActionResult> ValidarOtp([FromBody] ValidarOtpDTO dto)
        {
            var solicitacao = await _context.solicitacoesMatricula
                .FirstOrDefaultAsync(x => x.email == dto.Email && x.codigoOtp == dto.Codigo);

            if (solicitacao == null)
                return BadRequest("E-mail ou código inválidos.");

            if (solicitacao.validadeOtp < DateTime.Now)
                return BadRequest("O código expirou. Solicite um novo.");

            if (solicitacao.status == StatusMatricula.Iniciado)
            {
                solicitacao.status = StatusMatricula.AguardandoDados;
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                mensagem = "Código validado com sucesso!",
                idSolicitacao = solicitacao.id,
                nome = solicitacao.nomeCompleto
            });
        }

        [HttpPut("dados-complementares")]
        public async Task<IActionResult> SalvarDadosComplementares([FromBody] MatriculaPasso2DTO dto)
        {
            var solicitacao = await _context.solicitacoesMatricula.FindAsync(dto.SolicitacaoId);

            if (solicitacao == null)
                return NotFound("Solicitação não encontrada.");

            solicitacao.enderecoCompleto = dto.EnderecoCompleto;
            solicitacao.rg = dto.Rg;
            solicitacao.nomeResponsavel = dto.NomeResponsavel;
            solicitacao.contatoResponsavel = dto.ContatoResponsavel;
            solicitacao.escolaridadeAnterior = dto.EscolaridadeAnterior;

            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Dados salvos com sucesso!" });
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocumento(
            [FromForm] int solicitacaoId,
            [FromForm] TipoDocumentoMatricula tipo,
            IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Arquivo inválido.");

            var solicitacao = await _context.solicitacoesMatricula.FindAsync(solicitacaoId);
            if (solicitacao == null) return NotFound("Solicitação não encontrada.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "matriculas", solicitacaoId.ToString());

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var extensao = Path.GetExtension(arquivo.FileName);
            var nomeArquivo = $"{tipo}_{Guid.NewGuid()}{extensao}";
            var caminhoCompleto = Path.Combine(uploadsFolder, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            var documento = new DocumentoMatricula
            {
                solicitacaoMatriculaId = solicitacaoId,
                tipo = tipo,
                caminhoArquivo = $"uploads/matriculas/{solicitacaoId}/{nomeArquivo}",
                nomeOriginalArquivo = arquivo.FileName,
                validado = false
            };

            _context.documentosMatricula.Add(documento);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Upload realizado com sucesso!", idDocumento = documento.id });
        }

        [HttpGet("vagas-disponiveis")]
        public async Task<IActionResult> ObterVagas()
        {
            var configuracoes = await _context.configuracoesVaga
                .Where(c => c.anoLetivo == 2026)
                .ToListAsync();

            if (!configuracoes.Any())
            {
                return NotFound("Nenhuma vaga configurada para o ano letivo atual.");
            }

            var ocupacoes = await _context.solicitacoesMatricula
                .Where(s => s.status != StatusMatricula.Rejeitado && s.serieDesejada != null)
                .Select(s => new { s.serieDesejada, s.turnoDesejado })
                .ToListAsync();

            var resultado = configuracoes.Select(config =>
            {
                int ocupadas = ocupacoes.Count(s =>
                    s.serieDesejada == config.serie &&
                    s.turnoDesejado == config.turno);

                return new
                {
                    opt = new
                    {
                        serie = config.serie,
                        turno = config.turno.ToString(),
                        valor = config.valorMensalidade
                    },
                    vagasRestantes = config.vagasTotais - ocupadas,
                    disponivel = (config.vagasTotais - ocupadas) > 0
                };
            });

            return Ok(resultado);
        }

        [HttpPut("selecionar-vaga")]
        public async Task<IActionResult> SelecionarVaga([FromBody] SelecaoVagaDTO dto)
        {
            var solicitacao = await _context.solicitacoesMatricula.FindAsync(dto.SolicitacaoId);

            if (solicitacao == null) return NotFound("Solicitação não encontrada.");

            decimal valorCalculado = 0;
            if (dto.Serie.Contains("1º")) valorCalculado = dto.Turno == "Manha" ? 1200 : 1100;
            else if (dto.Serie.Contains("2º")) valorCalculado = dto.Turno == "Manha" ? 1250 : 950;
            else if (dto.Serie.Contains("3º")) valorCalculado = 1300;

            if (!Enum.TryParse<Turno>(dto.Turno, true, out var turnoEnum))
            {
                return BadRequest("Turno inválido.");
            }

            solicitacao.serieDesejada = dto.Serie;
            solicitacao.turnoDesejado = turnoEnum;
            solicitacao.valorMensalidade = valorCalculado;

            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Vaga selecionada com sucesso!", valor = valorCalculado });
        }
    }
}