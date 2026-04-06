using edu_connect_backend.DTO.Professor;
using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class ProfessorMapper
    {

        public Professor ToProfessor(ProfessorRequestDTO dto)
        {
            return new Professor
            {
                matricula = dto.Matricula,
                especialidade = dto.Especialidade,
                usuario = new Usuario
                {
                    nome = dto.Nome,
                    email = dto.Email,
                    ativo = dto.Ativo
                }
            };
        }

        public ProfessorResponseDTO ToProfessorResponseDTO(Professor model)
        {
            return new ProfessorResponseDTO
            {
                Id = model.id,
                Nome = model.usuario != null ? model.usuario.nome : "N/A",
                Email = model.usuario != null ? model.usuario.email : "N/A",
                Matricula = model.matricula,
                Especialidade = model.especialidade,
                Ativo = model.usuario != null ? model.usuario.ativo : false
            };
        }

        public List<ProfessorResponseDTO> ToProfessorResponseDTOList(List<Professor> models)
        {
            return models.Select(ToProfessorResponseDTO).ToList();
        }
    }
}