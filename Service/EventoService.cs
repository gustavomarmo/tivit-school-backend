using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class EventoService
    {
        private readonly NotificacaoRepository notificacaoRepository;
        private readonly AlunoRepository alunoRepository;
        private readonly EventoRepository eventoRepository;

        public EventoService(
            EventoRepository eventoRepository,
            NotificacaoRepository notificacaoRepository,
            AlunoRepository alunoRepository)
        {
            this.eventoRepository = eventoRepository;
            this.notificacaoRepository = notificacaoRepository;
            this.alunoRepository = alunoRepository;
        }

        public void CriarEvento(Evento evento)
        {
            eventoRepository.Criar(evento);

            var alunos = alunoRepository.ObterAlunos(null);

            var alunosParaNotificar = evento.turmaId.HasValue
                ? alunos.Where(a => a.turmaId == evento.turmaId).ToList()
                : alunos;

            foreach (var aluno in alunosParaNotificar)
            {
                notificacaoRepository.CriarParaAluno(
                    alunoId: aluno.id,
                    tipo: "info",
                    titulo: "Novo Evento no Calendário",
                    mensagem: $"O evento \"{evento.titulo}\" foi adicionado ao calendário."
                );
            }
        }

        public List<Evento> ListarEventos(int mes, int ano)
        {
            return eventoRepository.ObterPorMesAno(mes, ano)
                ?? throw new KeyNotFoundException("Nenhum evento encontrado");
        }

        public void EditarEvento(int id, Evento dadosAtualizados)
        {
            var evento = eventoRepository.ObterPorId(id)
                ?? throw new KeyNotFoundException("Evento não encontrado");

            evento.titulo = dadosAtualizados.titulo;
            evento.descricao = dadosAtualizados.descricao;
            evento.dataInicio = dadosAtualizados.dataInicio;
            evento.dataFim = dadosAtualizados.dataFim;
            evento.tipo = dadosAtualizados.tipo;
            evento.turmaId = dadosAtualizados.turmaId;

            eventoRepository.Atualizar(evento);
        }

        public void DeletarEvento(int id)
        {
            var evento = eventoRepository.ObterPorId(id)
                ?? throw new KeyNotFoundException("Evento não encontrado");

            eventoRepository.Deletar(evento);
        }
    }
}