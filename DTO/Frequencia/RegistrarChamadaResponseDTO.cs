using static System.Runtime.InteropServices.JavaScript.JSType;

namespace edu_connect_backend.DTO.Frequencia
{
    public class RegistrarChamadaResponseDTO
    {
        public int TurmaId { get; set; }
        public int DisciplinaId { get; set; }
        public Date Data { get; set; }
        public List<ChamadaRequestDTO> Chamada { get; set; }
    }
}
