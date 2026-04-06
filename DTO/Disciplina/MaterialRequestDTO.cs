namespace edu_connect_backend.DTO.Disciplina
{
    public class TopicoRequestDTO
    {
        public string Titulo { get; set; }
        public int TurmaDisciplinaId { get; set; }
    }

    public class MaterialRequestDTO
    {
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string? Url { get; set; }
        public int TopicoId { get; set; }
    }
}