using edu_connect_backend.Context;
using edu_connect_backend.DTO;
using edu_connect_backend.Model;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class NotaService
    {
        private readonly NotaRepository _repository;
        private readonly AlunoRepository _alunoRepository;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly ConnectionContext _context;

        public NotaService(
            NotaRepository repository,
            AlunoRepository alunoRepository,
            UsuarioRepository usuarioRepository,
            ConnectionContext context)
        {
            _repository = repository;
            _alunoRepository = alunoRepository;
            _usuarioRepository = usuarioRepository;
            _context = context;
        }

        public void LancarNota(NotaRequestDTO dto)
        {
            var vinculo = _repository.ObterTurmaDisciplina(dto.TurmaId, dto.DisciplinaId);

            if (vinculo == null)
                throw new Exception("Esta disciplina não está vinculada a esta turma.");

            var notaExistente = _repository.ObterNotaEspecifica(dto.AlunoId, vinculo.id, dto.Bimestre, dto.Tipo);

            if (notaExistente != null)
            {
                notaExistente.valor = dto.Valor;
                notaExistente.dataLancamento = DateTime.Now;
                _repository.Atualizar(notaExistente);
            }
            else
            {
                var novaNota = new Nota
                {
                    alunoId = dto.AlunoId,
                    turmaDisciplinaId = vinculo.id,
                    valor = dto.Valor,
                    bimestre = dto.Bimestre,
                    tipo = dto.Tipo,
                    descricao = $"Nota {dto.Tipo} - {dto.Bimestre}º Bimestre",
                    dataLancamento = DateTime.Now
                };
                _repository.Salvar(novaNota);
            }
        }

        public List<BoletimDTO>? obterBoletim(string emailUsuario)
        {
            var usuario = _usuarioRepository.obterUsuarioPorEmail(emailUsuario);
            if (usuario == null) return null;

            var aluno = _alunoRepository.ObterPorUsuarioId(usuario.id);

            if (aluno == null) return null;

            return _repository.ObterBoletimPorAluno(aluno.id);
        }

        public List<NotaLancamentoDTO> obterListaLancamento(int turmaId, int disciplinaId, int bimestre)
        {
            return _repository.ObterAlunosParaLancamento(turmaId, disciplinaId, bimestre);
        }

        public void lancarNotasEmLote(List<NotaRequestDTO> listaNotas)
        {
            if (listaNotas == null || !listaNotas.Any()) return;

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                foreach (var dto in listaNotas)
                {
                    LancarNota(dto);
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