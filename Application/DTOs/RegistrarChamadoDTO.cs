using static System.Runtime.InteropServices.JavaScript.JSType;

namespace edu_connect_backend.Application.DTOs
{
    public class RegistrarChamadoDTO
    {
        public int turmaId { get; set; }
        public int disciplinaId { get; set; }
        public Date data { get; set; }
        public List<ChamadaRequestDTO> chamada { get; set; }
    }
}
