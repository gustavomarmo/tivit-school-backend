namespace edu_connect_backend.DTO.Disciplina
{
    public class DisciplinaResumoDTO
    {
        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public int TurmaId { get; set; }
        public string Nome { get; set; }
        public string Turma { get; set; }
        public string Professor { get; set; }
    }

    public class DisciplinaConteudoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<TopicoDTO> Topicos { get; set; } = new();
    }

    public class TopicoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<MaterialDTO> Materiais { get; set; } = new();
    }

    public class MaterialDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string Url { get; set; }
        public DateTime? DataEntrega { get; set; }
        public bool Entregue { get; set; }
    }
}