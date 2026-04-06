namespace edu_connect_backend.DTO.Turma
{
    public class VinculoTurmaResponseDTO
    {
        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public string Disciplina { get; set; } = string.Empty;
        public int ProfessorId { get; set; }
        public string Professor { get; set; } = string.Empty;
    }
}
