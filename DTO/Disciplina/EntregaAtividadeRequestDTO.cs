using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO.Disciplina
{
    public class EntregaAtividadeRequestDTO
    {
        [Required(ErrorMessage = "O ID da atividade é obrigatório")]
        public int AtividadeId { get; set; }

        [Required(ErrorMessage = "O arquivo é obrigatório")]
        public IFormFile Arquivo { get; set; }
    }
}