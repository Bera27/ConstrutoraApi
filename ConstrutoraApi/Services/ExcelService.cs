using System.Data;
using System.Globalization;
using System.Text;
using ClosedXML.Excel;
using ConstrutoraApi.Data;
using ConstrutoraApi.Interfaces;
using ConstrutoraApi.Models;
using ExcelDataReader;

namespace ConstrutoraApi.Services
{
    public class ExcelService : IExcelService
    {
        private readonly ConstrutoraDataContext _context;

        public ExcelService(ConstrutoraDataContext context)
        {
            _context = context;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public byte[] GerarRelatorioOrcamento(List<OrcamentoObra> orcamentos)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.AddWorksheet("Custos de Obra");

            // Cria o cabeçalho
            string[] cabecalhos = { "Item", "Código", "Serviço", "Descrição", "UnidadeMedida", "Qtd", "CustoMat", "CustoMO", "CustoEquip", "CustoUnitTotal", "CustoTotal", "BDI", "PreçoUnit", "PreçoTotal", "Peso" };
            
            for (int i = 0; i < cabecalhos.Length; i++)
                worksheet.Cell(1, i + 1).Value = cabecalhos[i];

            int linha = 2;
            foreach (var item in orcamentos)
            {
                worksheet.Cell(linha, 1).Value = item.Item;
                worksheet.Cell(linha, 2).Value = item.Codigo;
                worksheet.Cell(linha, 3).Value = item.Servico;
                worksheet.Cell(linha, 4).Value = item.Descricao;
                worksheet.Cell(linha, 5).Value = item.UnidadeMedida;
                worksheet.Cell(linha, 6).Value = item.Qtd;
                worksheet.Cell(linha, 7).Value = item.CustoMat;
                worksheet.Cell(linha, 8).Value = item.CustoMO;
                worksheet.Cell(linha, 9).Value = item.CustoEquip;
                worksheet.Cell(linha, 10).Value = item.CustoUnitTotal;
                worksheet.Cell(linha, 11).Value = item.CustoTotal;
                worksheet.Cell(linha, 12).Value = item.Bdi;
                worksheet.Cell(linha, 13).Value = item.PrecoUnit;
                worksheet.Cell(linha, 14).Value = item.PrecoTotal;
                worksheet.Cell(linha, 15).Value = item.Peso;
                linha++;
            }

            // Cria a tabela e aplica tema
            int lastRow = linha - 1;
            var range = worksheet.Range(1, 1, lastRow, cabecalhos.Length);
            var table = range.CreateTable("TabelaCustos");

            table.Theme = XLTableTheme.TableStyleMedium9;
            table.ShowAutoFilter = true;
            table.ShowTotalsRow = true;

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var conteudo = stream.ToArray();

            return conteudo;
        }

        public async Task<PlanilhaObra> ImportarPlanilhaAsync(IFormFile arquivo, string nome, DateTime prazo)
        {
            var planilha = new PlanilhaObra
            {
                Nome = nome,
                Prazo = prazo
            };

            using var stream = arquivo.OpenReadStream();
            using var reader = ExcelReaderFactory.CreateReader(stream);

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

                planilha.Obras.Add(orcamento);
            }

            await _context.PlanilhaObra.AddAsync(planilha);
            await _context.SaveChangesAsync();

            return planilha;
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