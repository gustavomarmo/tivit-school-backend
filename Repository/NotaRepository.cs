using edu_connect_backend.Context;
using edu_connect_backend.DTO;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class NotaRepository
    {
        private readonly ConnectionContext _context;

        public NotaRepository(ConnectionContext context)
        {
            _context = context;
        }

        public List<BoletimDTO> ObterBoletimPorAluno(int alunoId)
        {
            // O EF Core mapeia automaticamente as colunas do SELECT para as propriedades do DTO
            return _context.Database
                .SqlQueryRaw<BoletimDTO>("EXEC sp_Notas_Boletim @AlunoId = {0}", alunoId)
                .ToList();
        }

        public List<NotaLancamentoDTO> ObterAlunosParaLancamento(int turmaId, int disciplinaId, int bimestre)
        {
            return _context.Database
                .SqlQueryRaw<NotaLancamentoDTO>(
                    "EXEC sp_Notas_Lancamento @TurmaId = {0}, @DisciplinaId = {1}, @Bimestre = {2}",
                    turmaId, disciplinaId, bimestre)
                .ToList();
        }
    }
}