using edu_connect_backend.Context;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class NotaService
    {
        private readonly NotaRepository notaRepository;
        private readonly AlunoRepository alunoRepository;
        private readonly UsuarioRepository usuarioRepository;
        private readonly ConnectionContext context;

        public NotaService(
            NotaRepository notaRepository,
            AlunoRepository alunoRepository,
            UsuarioRepository usuarioRepository,
            ConnectionContext context)
        {
            this.notaRepository = notaRepository;
            this.alunoRepository = alunoRepository;
            this.usuarioRepository = usuarioRepository;
            this.context = context;
        }

        public void LancarNota(Nota nota)
        {
            var vinculo = notaRepository.ObterTurmaDisciplina(nota.TempTurmaId, nota.TempDisciplinaId);

            if (vinculo == null)
                throw new Exception("Esta disciplina não está vinculada a esta turma.");

            var notaExistente = notaRepository.ObterNotaEspecifica(nota.alunoId, vinculo.id, nota.bimestre, nota.tipo);

            if (notaExistente != null)
            {
                notaExistente.valor = nota.valor;
                notaExistente.dataLancamento = DateTime.Now;
                notaRepository.Atualizar(notaExistente);
            }
            else
            {
                nota.turmaDisciplinaId = vinculo.id;
                nota.descricao = $"Nota {nota.tipo} - {nota.bimestre}º Bimestre";
                nota.dataLancamento = DateTime.Now;

                notaRepository.Salvar(nota);
            }
        }

        public List<BoletimReadModel>? obterBoletim(string emailUsuario)
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmail(emailUsuario);
            if (usuario == null) return null;

            var aluno = alunoRepository.ObterAlunoPorUsuarioId(usuario.id);
            if (aluno == null) return null;

            return notaRepository.ObterBoletimPorAluno(aluno.id);
        }

        public List<NotaLancamentoReadModel> obterListaLancamento(int turmaId, int disciplinaId, int bimestre)
        {
            return notaRepository.ObterAlunosParaLancamento(turmaId, disciplinaId, bimestre);
        }

        public void lancarNotasEmLote(List<Nota> listaNotas)
        {
            if (listaNotas == null || !listaNotas.Any()) return;

            using var transaction = context.Database.BeginTransaction();

            try
            {
                foreach (var nota in listaNotas)
                {
                    LancarNota(nota);
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}