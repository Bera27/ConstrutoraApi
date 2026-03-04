using System.Data;
using System.Globalization;
using System.Text;
using ConstrutoraApi.Data;
using ConstrutoraApi.Models;
using ConstrutoraApi.ViewModels;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;

namespace ConstrutoraApi.Controllers
{
    [Route("api/")]
    public class OrcamentoController : ControllerBase
    {
        public OrcamentoController()
        => Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        [HttpPost("v1/importar")]
        public async Task<IActionResult> ImportarPlanilha(IFormFile arquivo,
        [FromServices] ConstrutoraDataContext context)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest(new ResultViewModel<OrcamentoObra>("Nenhum Arquivo Enviado"));

            if (!arquivo.FileName.EndsWith(".xls") && !arquivo.FileName.EndsWith(".xlsx"))
                return BadRequest(new ResultViewModel<OrcamentoObra>("Formato invalido. Envie um arquivo .xls ou .xlsx"));

            try
            {
                using (var stream = arquivo.OpenReadStream())
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        // A configuração abaixo indica que a PRIMEIRA LINHA contém os cabeçalhos
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        var tabela = result.Tables[0];

                        foreach (DataRow row in tabela.Rows)
                        {
                            // Validação para pular linhas vazias
                            if (string.IsNullOrWhiteSpace(row[0]?.ToString()))
                                continue;

                            var orcamento = new OrcamentoObra
                            {
                                Item = row[0].ToString(),
                                Codigo = row[1].ToString(),
                                Servico = row[2].ToString(),
                                Descricao = row[3].ToString(),
                                UnidadeMedida = row[4].ToString(),
                                Qtd = ObterDecimal(row[5]),
                                CustoMat = ObterDecimal(row[6]),
                                CustoMO = ObterDecimal(row[7]),
                                CustoEquip = ObterDecimal(row[8]),
                                CustoUnitTotal = ObterDecimal(row[9]),
                                CustoTotal = ObterDecimal(row[10]),
                                Bdi = ObterDecimal(row[11]),
                                PrecoUnit = ObterDecimal(row[12]),
                                PrecoTotal = ObterDecimal(row[13]),
                                Peso = ObterDecimal(row[14])
                            };

                            await context.OrcamentosObras.AddAsync(orcamento);
                        }

                        await context.SaveChangesAsync();
                    }
                }

                return Created($"v1/importar/{arquivo}", new ResultViewModel<string>(arquivo.FileName));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<string>($"Erro ao processar a planilha: {ex.Message}"));
            }
        }

        // Helper para converter os valores
        private decimal ObterDecimal(object valor)
        {
            if (valor == null || string.IsNullOrWhiteSpace(valor.ToString()))
                return 0m;

            if (decimal.TryParse(valor.ToString(), NumberStyles.Number, CultureInfo.CurrentCulture, out decimal resultado))
                return resultado;

            return 0m;
        }
    }
}

