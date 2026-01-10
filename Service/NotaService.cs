using edu_connect_backend.DTO;
using edu_connect_backend.Repository;

namespace edu_connect_backend.Service
{
    public class NotaService
    {
        private readonly NotaRepository _repository;
        private readonly AlunoRepository _alunoRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public NotaService(
            NotaRepository repository,
            AlunoRepository alunoRepository,
            UsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _alunoRepository = alunoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public List<BoletimDTO>? ObterBoletim(string emailUsuario)
        {
            // 1. Acha o usuário pelo email (vindo do Token)
            var usuario = _usuarioRepository.ObterPorEmail(emailUsuario);
            if (usuario == null) return null;

            // 2. Acha o aluno vinculado a esse usuário
            var aluno = _alunoRepository.ObterAlunos(usuario.nome)
                            .FirstOrDefault(a => a.usuarioId == usuario.id);

            if (aluno == null) return null;

            // 3. Busca o boletim
            return _repository.ObterBoletimPorAluno(aluno.id);
        }

        public List<NotaLancamentoDTO> ObterListaLancamento(int turmaId, int disciplinaId, int bimestre)
        {
            return _repository.ObterAlunosParaLancamento(turmaId, disciplinaId, bimestre);
        }
    }
}