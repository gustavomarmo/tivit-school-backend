namespace edu_connect_backend.DTO.Matricula
{
    public class SelecaoVagaRequestDTO
    {
        public int SolicitacaoId { get; set; }
        public string Serie { get; set; } = string.Empty;
        public string Turno { get; set; } = string.Empty;
    }
}