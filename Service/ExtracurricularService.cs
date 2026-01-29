using edu_connect_backend.Context;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class ExtracurricularService
    {
        private readonly ExtracurricularRepository extracurricularRepository;
        private readonly UsuarioRepository usuarioRepository;
        private readonly AlunoRepository alunoRepository;

        public ExtracurricularService(
            ExtracurricularRepository extracurricularRepository,
            UsuarioRepository usuarioRepository,
            AlunoRepository alunoRepository)
        {
            this.extracurricularRepository = extracurricularRepository;
            this.usuarioRepository = usuarioRepository;
            this.alunoRepository = alunoRepository;
        }

        public List<TurmaExtracurricular> ListarExtracurriculares(string emailUsuario)
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmail(emailUsuario)
                ?? throw new KeyNotFoundException("Usuário não encontrado.");


            if (usuario.perfil == PerfilUsuario.Aluno)
            {
                var aluno = alunoRepository.ObterAlunoPorUsuarioId(usuario.id)
                    ?? throw new KeyNotFoundException("Aluno não encontrado.");
                return extracurricularRepository.ObterExtracurricularesPorAluno(aluno.id)
                    ?? throw new KeyNotFoundException("Nenhuma extracurricular encontrada para o aluno.");
            }
            return new List<TurmaExtracurricular>();
        }

        public TurmaExtracurricular? ObterConteudoExtracurricular(int idVinculo)
        {
            return extracurricularRepository.ObterConteudoCompleto(idVinculo)
                ?? throw new KeyNotFoundException("Não há conteúdo na extracurricular em questão.");
        }

        public void CriarAtividadeExtracurricular(Extracurricular nova)
        {
            extracurricularRepository.CriarExtracurricular(nova);
        }

        public void EditarAtividadeExtracurricular(int id, Extracurricular dadosAtualizados)
        {
            var atividade = extracurricularRepository.ObterExtracurricularPorId(id)
                ?? throw new KeyNotFoundException("Nenhuma extracurricular encontrada.");

            atividade.nome = dadosAtualizados.nome;
            atividade.descricao = dadosAtualizados.descricao;

            extracurricularRepository.AtualizarExtracurricular(atividade);
        }

        public void DeletarAtividadeExtracurricular(int id)
        {
            var atividade = extracurricularRepository.ObterExtracurricularPorId(id)
                ?? throw new KeyNotFoundException("Nenhuma extracurricular encontrada.");

            extracurricularRepository.DeletarExtracurricular(atividade);
        }

        public void VincularExtracurricular(TurmaExtracurricular vinculo)
        {
            extracurricularRepository.VincularTurmaExtracurricular(vinculo);
        }

        public void RemoverVinculoExtracurricular(int idVinculo)
        {
            var vinculo = extracurricularRepository.ObterVinculoPorId(idVinculo)
                ?? throw new KeyNotFoundException("Nenhuma vínculo encontrado.");

            extracurricularRepository.DeletarVinculoExtracurricular(vinculo);
        }
    }
}