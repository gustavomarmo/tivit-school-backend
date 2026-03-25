namespace edu_connect_backend.Application.DTOs
{
    public class SelecaoVagaDTO
    {
        public int SolicitacaoId { get; set; }
        public string Serie { get; set; } = string.Empty;
        public string Turno { get; set; } = string.Empty;
    }
}