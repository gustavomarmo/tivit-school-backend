namespace edu_connect_backend.DTO
{
    public class DisciplinaResumoDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string turma { get; set; }
        public string professor { get; set; }
    }

    public class DisciplinaConteudoDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public List<TopicoDTO> topicos { get; set; } = new();
    }

    public class TopicoDTO
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public List<MaterialDTO> materiais { get; set; } = new();
    }

    public class MaterialDTO
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string tipo { get; set; }
        public string url { get; set; }
        public DateTime? dataEntrega { get; set; }
        public bool entregue { get; set; }
    }
}