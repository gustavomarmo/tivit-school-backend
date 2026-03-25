namespace edu_connect_backend.Application.DTOs
{
    public class ChamadaRequestDTO
    {
        public int disciplinaId { get; set; }
        public DateTime data { get; set; }
        public List<RegistroPresencaDTO> registros { get; set; }
    }

    public class RegistroPresencaDTO
    {
        public int alunoId { get; set; }
        public bool presente { get; set; }
    }
}