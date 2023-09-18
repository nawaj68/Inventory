using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace InventoryManagement.Core.Extensions
{
    public static class ExcelExtensions
    {
        public static void SetValue(this ExcelRange cell, object value, CellType cellType = CellType.String, bool valid = true, bool required = false, int? length = null, Color? color = null)
        {
            if (required)
                if (value == null || (value is string && string.IsNullOrEmpty((string)value)))
                    valid = false;

            if (value != null)
                if (value.ToString().IsValidString(required, length) == false)
                    valid = false;

            cell.Value = value;

            if (cellType == CellType.String)
                cell.Style.Numberformat.Format = "@";
            else if (cellType == CellType.Date)
                cell.Style.Numberformat.Format = DateTimeExtension.DefaultFormat;
            else if (cellType == CellType.Number)
                cell.Style.Numberformat.Format = "0";
            else if (cellType == CellType.Decimal)
                cell.Style.Numberformat.Format = "0.0";
            else
                cell.Style.Numberformat.Format = "";

            if (!valid)
            {
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(Color.Red);
            }

            if (color != null)
            {
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(color.GetValueOrDefault());
            }
        }

        public static string Value(this ExcelWorksheet sheet, int row, int column)
        {
            return sheet.Cells[row, column].Text.Trim();
        }

        #region Exctensions For Future Use

        //public static void SetColor(this ExcelRange cell, object value, Color? color = null)
        //{
        //    cell.Value = value;
        //    cell.Style.Numberformat.Format = "";

        //    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //    cell.Style.Fill.BackgroundColor.SetColor(color.GetValueOrDefault(Color.White));
        //}

        //public static ExcelWorksheet GetExcelWorksheet(ExcelPackage package, int sheetNumber = 0)
        //{
        //    ExcelWorksheet worksheet = null;
        //    ExcelWorkbook workbook = package?.Workbook;
        //    if (workbook != null && workbook.Worksheets.Count >= sheetNumber)
        //    {
        //        worksheet = workbook.Worksheets[sheetNumber];
        //    }
        //    return worksheet;
        //}

        //public static List<T> GetClassFromExcel<T>(ExcelWorksheet sheet, Dictionary<string, string> mapper = null, int fromRow = 0,
        //    int fromColumn = 1, int toColumn = 0) where T : class
        //{
        //    List<T> retList = new List<T>();
        //    var ws = sheet;
        //    toColumn = toColumn == 0 ? typeof(T).GetProperties().Count() : toColumn;
        //    fromRow = fromRow == 0 ? sheet.Dimension.Start.Row + 1 : fromRow;

        //    for (var rowNum = fromRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        //    {
        //        T objT = Activator.CreateInstance<T>();
        //        Type myType = typeof(T);
        //        PropertyInfo[] myProp = myType.GetProperties();

        //        var wsRow = ws.Cells[rowNum, fromColumn, rowNum, toColumn];

        //        for (int i = 0; i < myProp.Count(); i++)
        //        {
        //            string header = String.Empty;

        //            int columnIndex = fromColumn + i;

        //            if (mapper != null && mapper.Count > 0)
        //            {
        //                header = mapper[myProp[i].Name];
        //            }

        //            if (!string.IsNullOrWhiteSpace(header))
        //            {
        //                int? findColumnAddress = FindColumnAddress(sheet, header);
        //                if (findColumnAddress.HasValue)
        //                {
        //                    columnIndex = findColumnAddress.Value;
        //                }
        //            }

        //            string value = wsRow[rowNum, columnIndex].Text;
        //            myProp[i].SetValue(objT, value);
        //        }
        //        retList.Add(objT);
        //    }
        //    return retList;

        //}

        //public static int? FindColumnAddress(ExcelWorksheet sheet, string columnName)
        //{
        //    var header = sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column, 1,
        //        sheet.Dimension.End.Column].Select(x => Regex.Replace(x.Text, @"\t+|\n+|\r+|\s+", "").Trim().ToLower()).ToList();

        //    columnName = Regex.Replace(columnName, @"\t+|\n+|\r+|\s+", "").Trim().ToLower();
        //    var idx = header.IndexOf(columnName);

        //    return idx < 0 ? (int?)null : idx + 1;
        //}

        //public static ExcelWorksheet GetDefaultExcelSheet(ExcelPackage package, string sheetName)
        //{
        //    ExcelWorksheet worksheet =
        //        package.Workbook.Worksheets.Add(sheetName);
        //    worksheet.TabColor = Color.Blue;
        //    worksheet.DefaultRowHeight = 12;
        //    worksheet.DefaultRowHeight = 18;
        //    worksheet.Row(1).Height = 20;
        //    worksheet.Row(1).Style.Font.Bold = true;

        //    return worksheet;
        //}
        //public static void CreateHeader(this ExcelWorksheet worksheet, Dictionary<string, string> dictionary)
        //{
        //    int cell = 1;
        //    foreach (KeyValuePair<string, string> valuePair in dictionary)
        //    {
        //        worksheet.Cells[1, cell].Value = valuePair.Value;
        //        cell++;
        //    }

        //}

        //public static ExcelPackage PrepareExcelFromData(ExcelPackage package, DataTable data, int fromRow = 1, int toRow = 0, int fromCol = 1,
        //    int toColumn = 0, string sheetName = null)
        //{
        //    ExcelWorksheet worksheet =
        //        package.Workbook.Worksheets.Add(string.IsNullOrWhiteSpace(sheetName) ? "Sheet1" : sheetName);
        //    worksheet.DefaultRowHeight = 12;
        //    worksheet.DefaultRowHeight = 18;
        //    worksheet.Row(1).Height = 20;
        //    worksheet.Row(1).Style.Font.Bold = true;
        //    List<DataColumn> column = new List<DataColumn>();
        //    column.AddRange(data.Columns.Cast<DataColumn>().AsEnumerable());
        //    bool needColumnDelete = false;

        //    fromCol = fromCol - 1;
        //    fromRow = fromRow - 1;
        //    if (fromRow != 0)
        //    {

        //        data = data.AsEnumerable().Skip(fromRow).CopyToDataTable();
        //    }
        //    if (toRow != 0)
        //    {
        //        data = data.AsEnumerable().Take(toRow - fromRow).CopyToDataTable();
        //    }
        //    if (fromCol != 0)
        //    {
        //        column = column.Skip(fromCol).ToList();
        //        needColumnDelete = true;
        //    }
        //    if (toColumn != 0)
        //    {
        //        needColumnDelete = true;
        //        column = column.Take(toColumn - fromCol).ToList();
        //    }

        //    if (needColumnDelete)
        //    {
        //        var enumerable = data.Columns.Cast<DataColumn>().AsEnumerable()
        //             .Where(x => !column.Select(y => y.ColumnName).Contains(x.ColumnName)).Select(x => x.ColumnName).ToList();
        //        if (enumerable.Any())
        //        {
        //            foreach (string columnName in enumerable)
        //            {
        //                data.Columns.Remove(columnName);
        //            }
        //        }
        //    }

        //    worksheet.Cells[1, 1, data.Rows.Count == 0 ? 1 : data.Rows.Count, data.Columns.Count].LoadFromDataTable(data, true);
        //    worksheet.Cells.AutoFitColumns();
        //    return package;
        //}
        //public static ExcelPackage PrepareExcelFromData<T>(ExcelPackage package, List<T> data, int fromRow = 1, int toRow = 0, int fromCol = 1,
        //    int toColumn = 0, string sheetName = null)
        //{
        //    var dataTable = data.ToDataTable();
        //    return PrepareExcelFromData(package, dataTable, fromRow, toRow, fromCol, toColumn, sheetName);
        //}
        //public static DataTable ToDataTable<T>(this IList<T> data)
        //{
        //    PropertyDescriptorCollection properties =
        //        TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    foreach (PropertyDescriptor prop in properties)
        //        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    foreach (T item in data)
        //    {
        //        DataRow row = table.NewRow();
        //        foreach (PropertyDescriptor prop in properties)
        //            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        //        table.Rows.Add(row);
        //    }
        //    return table;
        //}

        //public static byte[] WriteExcel<T>(this List<T> model, bool showHeaders) where T : new()
        //{
        //    using (ExcelPackage excelPackage = new ExcelPackage())
        //    {
        //        ExcelWorkbook workbook = excelPackage.Workbook;
        //        if (workbook != null)
        //        {
        //            ExcelWorksheet worksheet = workbook.Worksheets.Add("Sheet1");
        //            if (worksheet != null)
        //            {
        //                worksheet.Cells[1, 1].LoadFromCollection(Collection: model, PrintHeaders: showHeaders);
        //                worksheet.HeaderColumnsFormatted();
        //                worksheet.Cells.AutoFitColumns();
        //                return excelPackage.GetAsByteArray();
        //            }
        //        }
        //    }

        //    return null;
        //}


        //public static ExcelRange HeaderColumns(this ExcelWorksheet sheet)
        //{
        //    return sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column, 1, sheet.Dimension.End.Column];
        //}

        //public static ExcelRange HeaderColumnsFormatted(this ExcelWorksheet sheet)
        //{

        //    var firstRow = sheet.Dimension.Start.Row;
        //    var firstColumn = sheet.Dimension.Start.Column;
        //    var lastRow = 1;
        //    var lastColumn = sheet.Dimension.End.Column;

        //    var range = sheet.Cells[firstRow, firstColumn, lastRow, lastColumn];
        //    for (int i = 1; i <= lastColumn; i++)
        //    {
        //        range[firstRow, i].Value = range[firstRow, i].Value.ToString().ToUpper();
        //        range[firstRow, i].Style.Font.Bold = true;
        //    }

        //    return range;
        //}

        //public static string[] GetHeaderColumns(this ExcelWorksheet sheet)
        //{
        //    return sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column, 1, sheet.Dimension.End.Column]
        //        .Select(firstRowCell => firstRowCell.Text).ToArray();
        //}

        #endregion
    }

    public enum CellType
    {
        String,
        Date,
        Number,
        Decimal
    }
}
