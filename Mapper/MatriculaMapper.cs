using edu_connect_backend.DTO.Matricula;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class MatriculaMapper
    {
        public SolicitacaoMatricula ToSolicitacaoMatricula(MatriculaInicialRequestDTO dto)
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

        public SolicitacaoMatricula ToDadosComplementares(MatriculaPasso2RequestDTO dto)
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

        public SolicitacaoMatricula ToSelecaoVaga(SelecaoVagaRequestDTO dto)
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
                IdSolicitacao = model.id,
                Status = model.status.ToString(),
                Nome = model.nomeCompleto,
                Email = model.email,
                Cpf = model.cpf,
                Telefone = model.telefone,
                Endereco = model.enderecoCompleto,
                NomeResponsavel = model.nomeResponsavel,
                ContatoResponsavel = model.contatoResponsavel,
                Escolaridade = model.escolaridadeAnterior,
                Serie = model.serieDesejada,
                Turno = model.turnoDesejado?.ToString(),
                Mensalidade = model.valorMensalidade
            };
        }

        public MatriculaPendenteResponseDTO ToMatriculaPendenteResponseDTO(SolicitacaoMatricula model)
        {
            return new MatriculaPendenteResponseDTO
            {
                Id = model.id,
                Nome = model.nomeCompleto,
                Email = model.email,
                Cpf = model.cpf,
                Telefone = model.telefone,
                NomeResponsavel = model.nomeResponsavel,
                Serie = model.serieDesejada,
                Turno = model.turnoDesejado?.ToString(),
                Mensalidade = model.valorMensalidade,
                Status = model.status.ToString(),
                DataSolicitacao = model.dataSolicitacao,
                Documentos = model.documentos.Select(d => new DocumentoMatriculaResponseDTO
                {
                    Id = d.id,
                    Tipo = d.tipo.ToString(),
                    Url = d.caminhoArquivo,
                    NomeOriginal = d.nomeOriginalArquivo,
                    Validado = d.validado
                }).ToList()
            };
        }

        public List<MatriculaPendenteResponseDTO> ToMatriculaPendenteResponseDTOList(List<SolicitacaoMatricula> models)
        {
            return models.Select(ToMatriculaPendenteResponseDTO).ToList();
        }
    }
}