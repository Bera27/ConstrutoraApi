namespace ConstrutoraApi.Models
{
    public class UnidadeMedida
    {
        public int Id { get; set; }
        public string Sigla { get; set; } = string.Empty;

        public ICollection<OrcamentoObra> Orcamentos { get; set; } = [];
    }
}
