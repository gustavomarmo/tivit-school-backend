using System.ComponentModel.DataAnnotations;

namespace edu_connect_backend.DTO
{
    public class AlunoRequestDTO
    {
        public string nome { get; set; }

        public string matricula { get; set; }

        public int? turmaId { get; set; }

        public bool ativo { get; set; } = true;
    }
}
