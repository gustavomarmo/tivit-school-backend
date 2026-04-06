using Microsoft.AspNetCore.Http;

namespace edu_connect_backend.DTO.Disciplina
{
    public class EntregaAtividadeRequestDTO
    {
        public int AtividadeId { get; set; }
        public IFormFile Arquivo { get; set; }
    }
}