using System.Data;
using ConstrutoraApi.Data;
using ConstrutoraApi.Interfaces;
using ConstrutoraApi.Models;
using ConstrutoraApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstrutoraApi.Controllers
{
    [Route("api/")]
    public class OrcamentoController : ControllerBase
    {
        private readonly ConstrutoraDataContext _context;
        private readonly IExcelService _excelService;

        public OrcamentoController(ConstrutoraDataContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
        }

        [HttpGet("v1/orcamentos")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var planilhas = await _context.PlanilhaObra
                                .AsNoTracking()
                                .Include(x => x.Obras)
                                .ToListAsync();

                return Ok(new ResultViewModel<List<PlanilhaObra>>(planilhas));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<string>($"ESAX10 - Erro ao carregar as planilhas: {ex.Message}"));
            }
        }

        [HttpGet("v1/orcamento/{id:int}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id)
        {
            try
            {
                var planilha = await _context.PlanilhaObra
                                    .AsNoTracking()
                                    .Include(x => x.Obras)
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (planilha == null)
                    return NotFound(new ResultViewModel<string>("Planilha de orçamento de obra não encontrado"));

                return Ok(new ResultViewModel<PlanilhaObra>(planilha));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<string>($"ESWS20 - Erro ao carregar a planilha: {ex.Message}"));
            }
        }

        [HttpPost("v1/orcamentos/importar")]
        public async Task<IActionResult> ImportarPlanilha(
            IFormFile arquivo,
            [FromForm] string nome,
            [FromForm] DateTime prazo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest(new ResultViewModel<OrcamentoObra>("Nenhum Arquivo Enviado"));

            if (!arquivo.FileName.EndsWith(".xls") && !arquivo.FileName.EndsWith(".xlsx"))
                return BadRequest(new ResultViewModel<OrcamentoObra>("Formato invalido. Envie um arquivo .xls ou .xlsx"));

            try
            {
                var planilha = await _excelService.ImportarPlanilhaAsync(arquivo, nome, prazo);

                return Ok(new {message = $"Planilha '{nome}' importada com {planilha.Obras.Count} itens!"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<string>($"Erro ao processar a planilha: {ex.Message}"));
            }
        }

        [HttpGet("v1/orcamento/exportar/{id:int}")]
        public async Task<IActionResult> Exportar(
            [FromRoute] int id)
        {
            try
            {
                var orcamentos = await _context.OrcamentosObras
                                        .AsNoTracking()
                                        .Where(x => x.IdPlanilhaObra == id)
                                        .ToListAsync();

                if (orcamentos == null)
                    return NotFound(new ResultViewModel<string>("Planilha não encontrada!"));

                var conteudo = _excelService.GerarRelatorioOrcamento(orcamentos);

                return File(conteudo,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"Relatorio_Custos.xlsx");
            }
            catch(Exception ex)
            {
                return StatusCode(500, new ResultViewModel<string>($"Erro ao exportar a planilha: {ex.Message}"));
            }
        }

        [HttpDelete("v1/orcamento/{id:int}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id)
        {
            try
            {
                var planilha = await _context.PlanilhaObra.FirstOrDefaultAsync(x => x.Id == id);

                if (planilha == null)
                    return NotFound(new ResultViewModel<string>("Planilha de orçamento de obra não encontrado"));

                _context.PlanilhaObra.Remove(planilha);
                await _context.SaveChangesAsync();

                return Ok("Planilha removida com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<string>($"Erro ao remover a planilha: {ex.Message}"));
            }
        }
    }
}

