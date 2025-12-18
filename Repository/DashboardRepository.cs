using edu_connect_backend.Context;
using edu_connect_backend.DTO;
using Microsoft.EntityFrameworkCore;

namespace edu_connect_backend.Repository
{
    public class DashboardRepository
    {
        private readonly ConnectionContext context;

        public DashboardRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public List<NotaResumoDTO> ObterNotasRecentes(int alunoId)
        {
            // Mapeia o retorno da Procedure para o DTO
            return context.Database
                .SqlQueryRaw<NotaResumoDTO>("EXEC sp_Dashboard_NotasRecentes @AlunoId = {0}", alunoId)
                .ToList();
        }

        public List<AvisoResumoDTO> ObterAvisos(int turmaId)
        {
            return context.Database
                .SqlQueryRaw<AvisoResumoDTO>("EXEC sp_Dashboard_Avisos @TurmaId = {0}", turmaId)
                .ToList();
        }

        public List<TarefaPendenteDTO> ObterTarefasPendentes(int turmaId)
        {
            return context.Database
                .SqlQueryRaw<TarefaPendenteDTO>("EXEC sp_Dashboard_Tarefas @TurmaId = {0}", turmaId)
                .ToList();
        }
    }
}