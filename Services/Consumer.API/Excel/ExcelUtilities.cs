using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ClosedXML.Excel;
using Consumer.API.Entities;
using System.Text.Json;

namespace Consumer.API.Excel
{
    public class ExcelUtilities
    {
        public Stream DailyOrderLogsToExcel(List<Audit> audits)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");

                WriteHeadersToWorksheet(worksheet);
                WriteCevaPackagesToWorksheet(worksheet, audits);

                return GetWorkbookStream(workbook);
            }
        }
        private static void WriteHeadersToWorksheet(IXLWorksheet worksheet)
        {
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "EventType";
            worksheet.Cell(1, 3).Value = "OldOrder";
            worksheet.Cell(1, 4).Value = "NewOrder";
            worksheet.Cell(1, 5).Value = "CreatedAt";
        }
        private static void WriteCevaPackagesToWorksheet(IXLWorksheet worksheet, List<Audit> audits)
        {
            int startRowIndex = 2;

            for (int i = 0; i < audits.Count; i++)
            {
                int row = i + startRowIndex;

                Audit audit = audits[i];

                worksheet.Cell(row, 1).Value = audit.Id.ToString();
                worksheet.Cell(row, 2).Value = audit.EventType.ToString();
                worksheet.Cell(row, 3).Value = audit.OldOrder == null ? "" : JsonSerializer.Serialize(audit.OldOrder);
                worksheet.Cell(row, 4).Value = JsonSerializer.Serialize(audit.NewOrder);
                worksheet.Cell(row, 5).Value = audit.CreatedAt;
            }
        }
        private static Stream GetWorkbookStream(XLWorkbook workbook)
        {
            var stream = new MemoryStream();

            workbook.SaveAs(stream);

            stream.Position = 0;

            return stream;
        }
    }
}
