using edu_connect_backend.DTO;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class NotaMapper
    {
        public Nota ToNota(NotaRequestDTO dto)
        {
            return new Nota
            {
                alunoId = dto.AlunoId,
                valor = dto.Valor,
                bimestre = dto.Bimestre,
                tipo = dto.Tipo,
                TempTurmaId = dto.TurmaId,
                TempDisciplinaId = dto.DisciplinaId
            };
        }

        public List<Nota> ToNotaList(List<NotaRequestDTO> dtos)
        {
            return dtos.Select(ToNota).ToList();
        }

        public BoletimDTO ToBoletimDTO(BoletimReadModel model)
        {
            return new BoletimDTO
            {
                Materia = model.Materia,
                N1_N1 = model.N1_N1,
                N1_N2 = model.N1_N2,
                N1_Ativ = model.N1_Ativ,
                N2_N1 = model.N2_N1,
                N2_N2 = model.N2_N2,
                N2_Ativ = model.N2_Ativ
            };
        }

        public List<BoletimDTO> ToBoletimDTOList(List<BoletimReadModel> models)
        {
            return models.Select(ToBoletimDTO).ToList();
        }

        public NotaLancamentoDTO ToNotaLancamentoDTO(NotaLancamentoReadModel model)
        {
            return new NotaLancamentoDTO
            {
                AlunoId = model.AlunoId,
                Matricula = model.Matricula,
                Nome = model.Nome,
                N1_B1 = model.N1_B1,
                N2_B1 = model.N2_B1,
                Ativ_B1 = model.Ativ_B1,
                N1_B2 = model.N1_B2,
                N2_B2 = model.N2_B2,
                Ativ_B2 = model.Ativ_B2,
            };
        }

        public List<NotaLancamentoDTO> ToNotaLancamentoDTOList(List<NotaLancamentoReadModel> models)
        {
            return models.Select(ToNotaLancamentoDTO).ToList();
        }
    }
}