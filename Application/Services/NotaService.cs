using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Domain.Entities;
using edu_connect_backend.Infrastructure.Persistence.Context;
using edu_connect_backend.Infrastructure.Persistence.Repositories;

namespace edu_connect_backend.Application.Services
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
            var vinculo = notaRepository.ObterTurmaDisciplina(nota.TempTurmaId, nota.TempDisciplinaId)
                ?? throw new KeyNotFoundException("Vínculo não encontrado.");

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

            var aluno = context.alunos.FirstOrDefault(a => a.id == nota.alunoId);
            if (aluno != null)
            {
                var notificacao = new Notificacao
                {
                    usuarioId = aluno.usuarioId,
                    tipo = "success",
                    titulo = "Nova Nota Lançada",
                    mensagem = $"A sua nota de {nota.tipo} ({nota.bimestre}º Bimestre) de foi publicada/atualizada.",
                    dataCriacao = DateTime.Now,
                    lida = false
                };
                context.Notificacoes.Add(notificacao);
                context.SaveChanges();
            }
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