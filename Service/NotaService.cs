using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class NotaService
    {
        private readonly NotaRepository notaRepository;
        private readonly AlunoRepository alunoRepository;
        private readonly UsuarioRepository usuarioRepository;
        private readonly NotificacaoRepository notificacaoRepository;

        public NotaService(
            NotaRepository notaRepository,
            AlunoRepository alunoRepository,
            UsuarioRepository usuarioRepository,
            NotificacaoRepository notificacaoRepository)
        {
            this.notaRepository = notaRepository;
            this.alunoRepository = alunoRepository;
            this.usuarioRepository = usuarioRepository;
            this.notificacaoRepository = notificacaoRepository;
        }

        public void LancarNota(Nota nota)
        {
            var vinculo = notaRepository.ObterTurmaDisciplina(nota.TempTurmaId, nota.TempDisciplinaId)
                ?? throw new KeyNotFoundException("Vínculo turma-disciplina não encontrado.");

            var notaExistente = notaRepository.ObterNotaEspecifica(
                nota.alunoId, vinculo.id, nota.bimestre, nota.tipo);

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

            notificacaoRepository.CriarParaAluno(
                alunoId: nota.alunoId,
                tipo: "success",
                titulo: "Nova Nota Lançada",
                mensagem: $"A sua nota de {nota.tipo} ({nota.bimestre}º Bimestre) foi publicada/atualizada."
            );
        }

        public List<BoletimReadModel>? obterBoletim(string emailUsuario)
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmail(emailUsuario)
                ?? throw new KeyNotFoundException("Usuário não encontrado.");

            var aluno = alunoRepository.ObterAlunoPorUsuarioId(usuario.id)
                ?? throw new KeyNotFoundException("Aluno não encontrado.");

            return notaRepository.ObterBoletimPorAluno(aluno.id)
                ?? throw new KeyNotFoundException("Boletim não encontrado.");
        }

        public List<NotaLancamentoReadModel> obterListaLancamento(int turmaId, int disciplinaId)
        {
            return notaRepository.ObterAlunosParaLancamento(turmaId, disciplinaId)
                ?? throw new KeyNotFoundException("Alunos não encontrados.");
        }

        public void lancarNotasEmLote(List<Nota> listaNotas)
        {
            if (listaNotas == null || !listaNotas.Any()) return;

            notaRepository.ExecutarEmTransacao(() =>
            {
                foreach (var nota in listaNotas)
                    LancarNota(nota);
            });
        }
    }
}