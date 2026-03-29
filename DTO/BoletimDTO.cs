using System.Text.Json.Serialization;

namespace edu_connect_backend.DTO
{
    public class BoletimDTO
    {
        public string Materia { get; set; } = string.Empty;

        [JsonPropertyName("n1_n1")]
        public decimal N1_N1 { get; set; }

        [JsonPropertyName("n1_n2")]
        public decimal N1_N2 { get; set; }

        [JsonPropertyName("n1_ativ")]
        public decimal N1_Ativ { get; set; }

        [JsonPropertyName("n2_n1")]
        public decimal N2_N1 { get; set; }

        [JsonPropertyName("n2_n2")]
        public decimal N2_N2 { get; set; }

        [JsonPropertyName("n2_ativ")]
        public decimal N2_Ativ { get; set; }
    }
}