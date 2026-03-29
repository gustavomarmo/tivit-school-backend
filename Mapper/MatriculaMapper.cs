using edu_connect_backend.DTO;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class MatriculaMapper
    {
        public SolicitacaoMatricula ToSolicitacaoMatricula(MatriculaInicialDTO dto)
        {
            return new SolicitacaoMatricula
            {
                nomeCompleto = dto.NomeCompleto,
                cpf = dto.Cpf,
                email = dto.Email,
                telefone = dto.Telefone,
                dataNascimento = dto.DataNascimento,
                status = StatusMatricula.Iniciado,
                dataSolicitacao = DateTime.Now
            };
        }

        public SolicitacaoMatricula ToDadosComplementares(MatriculaPasso2DTO dto)
        {
            return new SolicitacaoMatricula
            {
                enderecoCompleto = dto.EnderecoCompleto,
                rg = dto.Rg,
                nomeResponsavel = dto.NomeResponsavel,
                contatoResponsavel = dto.ContatoResponsavel,
                escolaridadeAnterior = dto.EscolaridadeAnterior
            };
        }

        public SolicitacaoMatricula ToSelecaoVaga(SelecaoVagaDTO dto)
        {
            if (!Enum.TryParse<Turno>(dto.Turno, true, out var turnoEnum))
                throw new ArgumentException($"Turno inválido: {dto.Turno}. Use: Manha, Tarde ou Noite.");

            return new SolicitacaoMatricula
            {
                serieDesejada = dto.Serie,
                turnoDesejado = turnoEnum
            };
        }

        public DocumentoMatricula ToDocumentoMatricula(int solicitacaoId, TipoDocumentoMatricula tipo, string caminho, string nomeOriginal)
        {
            return new DocumentoMatricula
            {
                solicitacaoMatriculaId = solicitacaoId,
                tipo = tipo,
                caminhoArquivo = caminho,
                nomeOriginalArquivo = nomeOriginal,
                validado = false
            };
        }

        public MatriculaResponseDTO ToMatriculaResponseDTO(SolicitacaoMatricula model)
        {
            return new MatriculaResponseDTO
            {
                idSolicitacao = model.id,
                status = model.status.ToString(),
                nome = model.nomeCompleto,
                email = model.email,
                cpf = model.cpf,
                telefone = model.telefone,
                endereco = model.enderecoCompleto,
                nomeResponsavel = model.nomeResponsavel,
                contatoResponsavel = model.contatoResponsavel,
                escolaridade = model.escolaridadeAnterior,
                serie = model.serieDesejada,
                turno = model.turnoDesejado?.ToString(),
                mensalidade = model.valorMensalidade
            };
        }

        public MatriculaPendenteResponseDTO ToMatriculaPendenteResponseDTO(SolicitacaoMatricula model)
        {
            return new MatriculaPendenteResponseDTO
            {
                id = model.id,
                nome = model.nomeCompleto,
                email = model.email,
                cpf = model.cpf,
                telefone = model.telefone,
                nomeResponsavel = model.nomeResponsavel,
                serie = model.serieDesejada,
                turno = model.turnoDesejado?.ToString(),
                mensalidade = model.valorMensalidade,
                status = model.status.ToString(),
                dataSolicitacao = model.dataSolicitacao,
                documentos = model.documentos.Select(d => new DocumentoMatriculaResponseDTO
                {
                    id = d.id,
                    tipo = d.tipo.ToString(),
                    url = d.caminhoArquivo,
                    nomeOriginal = d.nomeOriginalArquivo,
                    validado = d.validado
                }).ToList()
            };
        }

        public List<MatriculaPendenteResponseDTO> ToMatriculaPendenteResponseDTOList(List<SolicitacaoMatricula> models)
        {
            return models.Select(ToMatriculaPendenteResponseDTO).ToList();
        }
    }
}