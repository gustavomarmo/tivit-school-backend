namespace edu_connect_backend.DTO.Frequencia
{
    public class ChamadaRequestDTO
    {
        public int DisciplinaId { get; set; }
        public DateTime Data { get; set; }
        public List<RegistroPresencaDTO> Registros { get; set; }
    }

    public class RegistroPresencaDTO
    {
        public int AlunoId { get; set; }
        public bool Presente { get; set; }
    }
}