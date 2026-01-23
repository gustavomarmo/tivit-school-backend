using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;
using System.Security.Claims;

namespace edu_connect_backend.Service
{
    public class EventoService
    {
        private readonly EventoRepository repository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public EventoService(EventoRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            this.httpContextAccessor = httpContextAccessor;
        }

        private int ObterIdUsuarioLogado()
        {
            var idClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id)) return id;
            throw new Exception("Usuário não identificado.");
        }

        public void CriarEvento(EventoRequestDTO dto)
        {
            var usuarioId = ObterIdUsuarioLogado();

            var evento = new Evento
            {
                titulo = dto.titulo,
                descricao = dto.descricao,
                dataInicio = dto.dataInicio,
                dataFim = dto.dataFim,
                tipo = dto.tipo,
                turmaId = dto.turmaId,
                usuarioCriadorId = usuarioId
            };

            repository.Criar(evento);
        }

        public List<EventoResponseDTO> ListarEventos(int mes, int ano)
        {
            var eventos = repository.ObterPorMesAno(mes, ano);

            return eventos.Select(e => new EventoResponseDTO
            {
                id = e.id,
                title = e.titulo,
                start = e.dataInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = e.dataFim?.ToString("yyyy-MM-ddTHH:mm:ss"),
                type = e.tipo,
                description = e.descricao,
                turmaNome = e.turma?.nome ?? "Geral"
            }).ToList();
        }

        public void EditarEvento(int id, EventoRequestDTO dto)
        {
            var evento = repository.ObterPorId(id);
            if (evento == null) throw new Exception("Evento não encontrado.");

            evento.titulo = dto.titulo;
            evento.descricao = dto.descricao;
            evento.dataInicio = dto.dataInicio;
            evento.dataFim = dto.dataFim;
            evento.tipo = dto.tipo;

            repository.Atualizar(evento);
        }

        public void DeletarEvento(int id)
        {
            var evento = repository.ObterPorId(id);
            if (evento == null) throw new Exception("Evento não encontrado.");

            repository.Deletar(evento);
        }
    }
}