using Microsoft.AspNetCore.Http;

namespace edu_connect_backend.Application.DTOs
{
    public class EntregaAtividadeDTO
    {
        public int AtividadeId { get; set; }
        public IFormFile Arquivo { get; set; }
    }
}