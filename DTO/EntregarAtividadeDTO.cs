using Microsoft.AspNetCore.Http; // Necessário para IFormFile

namespace edu_connect_backend.DTO
{
    public class EntregaAtividadeDTO
    {
        public int AtividadeId { get; set; }
        public IFormFile Arquivo { get; set; }
    }
}