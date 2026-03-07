namespace ConstrutoraApi.Models
{
    public class OrcamentoObra
    {
        public int Id { get; set; }
        public string Item { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Servico { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string UnidadeMedida { get; set; } = string.Empty;
        public decimal Qtd { get; set; }
        public decimal CustoMat { get; set; }
        public decimal CustoMO { get; set; }
        public decimal CustoEquip { get; set; }
        public decimal CustoUnitTotal { get; set; }
        public decimal CustoTotal { get; set; }
        public decimal Bdi { get; set; }
        public decimal PrecoUnit { get; set; }
        public decimal PrecoTotal { get; set; }
        public decimal Peso { get; set; }

        public int IdPlanilhaObra { get; set; }
        public PlanilhaObra PlanilhaObra { get; set; }
    }
}
