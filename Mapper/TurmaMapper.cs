using edu_connect_backend.Model;

namespace edu_connect_backend.Mapper
{
    public class TurmaMapper
    {
        public TurmaMapper()
        {
        }

        public List<string> ToTurmaNomesList(List<Turma> models)
        {
            return models.Select(t => t.nome).Distinct().ToList();
        }
    }
}