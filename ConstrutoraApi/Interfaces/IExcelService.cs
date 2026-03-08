using ConstrutoraApi.Models;

namespace ConstrutoraApi.Interfaces
{
    public interface IExcelService
    {
        Task<PlanilhaObra> ImportarPlanilhaAsync(IFormFile arquivo, string nome, DateTime prazo);
        byte[] GerarRelatorioOrcamento(List<OrcamentoObra> orcamentos);
    }
}