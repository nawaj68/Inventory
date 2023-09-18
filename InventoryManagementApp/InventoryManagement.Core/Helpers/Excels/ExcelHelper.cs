using InventoryManagement.Core.Extensions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Core.Helpers
{
    public static class ExcelHelper
    {
        public static byte[] WriteExcel<T>(this List<T> model, bool showHeaders = true) where T : new()
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorkbook workbook = excelPackage.Workbook;
                if (workbook != null)
                {
                    ExcelWorksheet worksheet = workbook.Worksheets.Add("Sheet1");
                    if (worksheet != null)
                    {
                        worksheet.Cells[1, 1].LoadFromCollection(Collection: model, PrintHeaders: showHeaders);
                        worksheet.HeaderColumnsFormatted();
                        worksheet.Cells.AutoFitColumns();
                        return excelPackage.GetAsByteArray();
                    }
                }
            }

            return Array.Empty<byte>();
        }


        public static ExcelRange HeaderColumnsFormatted(this ExcelWorksheet sheet)
        {
            var firstRow = sheet.Dimension.Start.Row;
            var firstColumn = sheet.Dimension.Start.Column;
            var lastRow = 1;
            var lastColumn = sheet.Dimension.End.Column;

            var range = sheet.Cells[firstRow, firstColumn, lastRow, lastColumn];
            for (int i = 1; i <= lastColumn; i++)
            {
                range[firstRow, i].Value = range[firstRow, i].Value.ToString().ToHeaderCase();
                range[firstRow, i].Style.Font.Bold = true;
            }

            return range;
        }

        public static string[] GetHeaderColumns(this ExcelWorksheet sheet)
        {
            return sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column, 1, sheet.Dimension.End.Column]
                .Select(firstRowCell => firstRowCell.Text).ToArray();
        }
       
        public static byte[] WriteExcel<T>(this List<T> model,Dictionary<string,string> formalaValues, bool showHeaders = true) where T : new()
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorkbook workbook = excelPackage.Workbook;
                if (workbook != null)
                {
                    ExcelWorksheet worksheet = workbook.Worksheets.Add("Sheet1");
                    if (worksheet != null)
                    {
                        worksheet.Cells[1, 1].LoadFromCollection(Collection: model, PrintHeaders: showHeaders);
                        worksheet.HeaderColumnsFormatted();
                        worksheet.Cells.AutoFitColumns();
                        foreach (var item in formalaValues)
                        { 
                            worksheet.Cells[item.Key].Value = item.Value;
                        }
                        return excelPackage.GetAsByteArray();
                    }
                }
            }

            return Array.Empty<byte>();
        }


    }
}
