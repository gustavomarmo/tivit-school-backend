using edu_connect_backend.DTO;
using edu_connect_backend.Model;

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
                id = model.id,
                nome = model.usuario != null ? model.usuario.nome : "N/A",
                email = model.usuario != null ? model.usuario.email : "N/A",
                matricula = model.matricula,
                turma = model.turma != null ? model.turma.nome : "Sem Turma",
                turmaId = model.turma != null ? model.turma.id : 0,
                ativo = model.usuario != null ? model.usuario.ativo : false
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
                matricula = dto.matricula,
                turmaId = dto.turmaId,
                usuario = new Usuario
                {
                    nome = dto.nome,
                    ativo = dto.ativo
                }
            };
        }
    }
}