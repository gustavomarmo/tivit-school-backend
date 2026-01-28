using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class TopicoService
    {
        private readonly TopicoRepository topicoRepository;

        public TopicoService(TopicoRepository topicoRepository)
        {
            this.topicoRepository = topicoRepository;
        }

        public void CriarTopico(TopicoRequestDTO dto)
        {
            var topico = new Topico { titulo = dto.titulo, turmaDisciplinaId = dto.turmaDisciplinaId };
            topicoRepository.CriarTopico(topico);
        }

        public void EditarTopico(int id, TopicoRequestDTO dto)
        {
            var topico = topicoRepository.ObterTopicoPorId(id);
            if (topico == null) throw new Exception("Tópico não encontrado.");

            topico.titulo = dto.titulo;
            topicoRepository.AtualizarTopico(topico);
        }

        public void DeletarTopico(int id)
        {
            var topico = topicoRepository.ObterTopicoPorId(id);
            if (topico == null) throw new Exception("Tópico não encontrado.");

            topicoRepository.DeletarTopico(topico);
        }
    }
}