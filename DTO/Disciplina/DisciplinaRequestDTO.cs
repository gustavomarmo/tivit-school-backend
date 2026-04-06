namespace edu_connect_backend.DTO.Disciplina
{
    public class DisciplinaCriacaoDTO
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }
    }

    public class VincularDisciplinaDTO
    {
        public int TurmaId { get; set; }
        public int DisciplinaId { get; set; }
        public int ProfessorId { get; set; }
    }
}
