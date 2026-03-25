namespace edu_connect_backend.Application.DTOs
{
    public class DisciplinaCriacaoDTO
    {
        public string nome { get; set; }
        public string codigo { get; set; }
    }

    public class VincularDisciplinaDTO
    {
        public int turmaId { get; set; }
        public int disciplinaId { get; set; }
        public int professorId { get; set; }
    }
}
