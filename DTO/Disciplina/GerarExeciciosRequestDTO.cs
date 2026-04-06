using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Disciplina
{
    public class GerarExerciciosRequestDTO
    {
        [Required(ErrorMessage = "O campo 'TextoConteudo' é obrigatório")]
        public string TextoConteudo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo 'NomeMaterial' é obrigatório")]
        public string NomeMaterial { get; set; } = string.Empty;
    }
}
