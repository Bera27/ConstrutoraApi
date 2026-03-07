using System.Collections;

namespace ConstrutoraApi.Models
{
    public class PlanilhaObra
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime Prazo { get; set; }

        public ICollection<OrcamentoObra> Obras { get; set; } = [];
    }
}