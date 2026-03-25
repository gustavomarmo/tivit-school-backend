using edu_connect_backend.Application.DTOs;
using edu_connect_backend.Domain.Entities;

namespace edu_connect_backend.Application.Mappers
{
    public class ProfessorMapper
    {

        public Professor ToProfessor(ProfessorRequestDTO dto)
        {
            return new Professor
            {
                matricula = dto.matricula,
                especialidade = dto.especialidade,
                usuario = new Usuario
                {
                    nome = dto.nome,
                    email = dto.email,
                    ativo = dto.ativo
                }
            };
        }

        public ProfessorResponseDTO ToProfessorResponseDTO(Professor model)
        {
            return new ProfessorResponseDTO
            {
                id = model.id,
                nome = model.usuario != null ? model.usuario.nome : "N/A",
                email = model.usuario != null ? model.usuario.email : "N/A",
                matricula = model.matricula,
                especialidade = model.especialidade,
                ativo = model.usuario != null ? model.usuario.ativo : false
            };
        }

        public List<ProfessorResponseDTO> ToProfessorResponseDTOList(List<Professor> models)
        {
            return models.Select(ToProfessorResponseDTO).ToList();
        }
    }
}