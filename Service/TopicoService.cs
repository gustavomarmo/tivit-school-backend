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

        public void CriarTopico(Topico topico)
        {
            topicoRepository.CriarTopico(topico);
        }

        public void EditarTopico(int id, Topico dadosAtualizados)
        {
            var topico = topicoRepository.ObterTopicoPorId(id);
            if (topico == null) throw new Exception("Tópico não encontrado.");

            topico.titulo = dadosAtualizados.titulo;

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