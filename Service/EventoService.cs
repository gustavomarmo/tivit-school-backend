using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class EventoService
    {
        private readonly EventoRepository eventoRepository;

        public EventoService(EventoRepository eventoRepository)
        {
            this.eventoRepository = eventoRepository;
        }

        public void CriarEvento(Evento evento)
        {
            eventoRepository.Criar(evento);
        }

        public List<Evento> ListarEventos(int mes, int ano)
        {
            return eventoRepository.ObterPorMesAno(mes, ano);
        }

        public void EditarEvento(int id, Evento dadosAtualizados)
        {
            var evento = eventoRepository.ObterPorId(id);
            if (evento == null) throw new Exception("Evento não encontrado.");

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
            var evento = eventoRepository.ObterPorId(id);
            if (evento == null) throw new Exception("Evento não encontrado.");

            eventoRepository.Deletar(evento);
        }
    }
}