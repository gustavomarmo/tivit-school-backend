using System.ComponentModel.DataAnnotations.Schema;

namespace edu_connect_backend.Model
{
    [Table("aviso")]
    public class Aviso
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string mensagem { get; set; }
        public DateTime dataPostagem { get; set; }

        // Se null, é aviso para escola toda. Se preenchido, é só para aquela turma.
        public int? turmaId { get; set; }
        public Turma? turma { get; set; }
    }
}