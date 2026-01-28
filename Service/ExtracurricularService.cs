using edu_connect_backend.Context;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class ExtracurricularService
    {
        private readonly ExtracurricularRepository extracurricularRepository;
        private readonly UsuarioRepository usuarioRepository;
        private readonly ConnectionContext context;

        public ExtracurricularService(
            ExtracurricularRepository extracurricularRepository,
            UsuarioRepository usuarioRepository,
            ConnectionContext context)
        {
            this.extracurricularRepository = extracurricularRepository;
            this.usuarioRepository = usuarioRepository;
            this.context = context;
        }

        public List<TurmaExtracurricular> ListarExtracurriculares(string emailUsuario)
        {
            var usuario = this.usuarioRepository.ObterUsuarioPorEmail(emailUsuario);
            if (usuario == null) return new List<TurmaExtracurricular>();

            if (usuario.perfil == PerfilUsuario.Aluno)
            {
                var aluno = this.context.alunos.FirstOrDefault(a => a.usuarioId == usuario.id);
                if (aluno != null)
                {
                    return this.extracurricularRepository.ObterExtracurricularesPorAluno(aluno.id);
                }
            }
            return new List<TurmaExtracurricular>();
        }

        public TurmaExtracurricular? ObterConteudoExtracurricular(int idVinculo)
        {
            return this.extracurricularRepository.ObterConteudoCompleto(idVinculo);
        }

        public void CriarAtividadeExtracurricular(Extracurricular nova)
        {
            extracurricularRepository.CriarExtracurricular(nova);
        }

        public void EditarAtividadeExtracurricular(int id, Extracurricular dadosAtualizados)
        {
            var atividade = extracurricularRepository.ObterExtracurricularPorId(id);
            if (atividade == null) throw new Exception("Atividade não encontrada.");

            atividade.nome = dadosAtualizados.nome;
            atividade.descricao = dadosAtualizados.descricao;

            extracurricularRepository.AtualizarExtracurricular(atividade);
        }

        public void DeletarAtividadeExtracurricular(int id)
        {
            var atividade = extracurricularRepository.ObterExtracurricularPorId(id);
            if (atividade == null) throw new Exception("Atividade não encontrada.");

            extracurricularRepository.DeletarExtracurricular(atividade);
        }

        public void VincularExtracurricular(TurmaExtracurricular vinculo)
        {
            extracurricularRepository.VincularTurmaExtracurricular(vinculo);
        }

        public void RemoverVinculoExtracurricular(int idVinculo)
        {
            var vinculo = extracurricularRepository.ObterVinculoPorId(idVinculo);
            if (vinculo == null) throw new Exception("Vínculo não encontrado.");

            extracurricularRepository.DeletarVinculoExtracurricular(vinculo);
        }
    }
}