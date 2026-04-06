using edu_connect_backend.DTO.Aluno;
using edu_connect_backend.Model;
using edu_connect_backend.Service;

namespace edu_connect_backend.Mapper
{
    public class AlunoMapper
    {
        public AlunoMapper()
        {
        }

        public AlunoResponseDTO ToAlunoResponseDTO(Aluno model)
        {
            return new AlunoResponseDTO
            {
                Id = model.id,
                Nome = model.usuario != null ? model.usuario.nome : "N/A",
                Email = model.usuario != null ? model.usuario.email : "N/A",
                Matricula = model.matricula,
                Turma = model.turma != null ? model.turma.nome : "Sem Turma",
                TurmaId = model.turma != null ? model.turma.id : 0,
                Ativo = model.usuario != null ? model.usuario.ativo : false
            };
        }

        public List<AlunoResponseDTO> ToAlunoResponseDTOList(List<Aluno> models)
        {
            return models.Select(ToAlunoResponseDTO).ToList();
        }

        public Aluno ToAluno(AlunoRequestDTO dto)
        {
            return new Aluno
            {
                matricula = dto.Matricula,
                turmaId = dto.TurmaId,
                usuario = new Usuario
                {
                    nome = dto.Nome,
                    email = dto.Email,
                    ativo = dto.Ativo
                }
            };
        }
    }
}