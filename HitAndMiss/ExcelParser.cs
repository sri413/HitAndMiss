using ClosedXML.Excel;

public class ExcelParser
{
    public List<int> W_three = new List<int>();
    public List<int> W_five = new List<int>();
    public List<double> W_threeWL = new List<double>();
    public List<double> W_fiveWL = new List<double>();
    public List<int> M_three = new List<int>();
    public List<int> M_five = new List<int>();
    public List<List<int>> Three_Combo = new List<List<int>>();
    public List<List<int>> Five_Combo = new List<List<int>>();
    private string xlFilename = "main.xlsx"; // Placeholder for your actual file path

    public static ExcelParser xlParserInstance = null;

    public static ExcelParser GetXlParserInstance
    {
        get
        {
            if (xlParserInstance == null)
            {
                xlParserInstance = new ExcelParser();
            }
            return xlParserInstance;
        }
    }


    public void ReadData()
    {
        using (var workbook = new XLWorkbook(xlFilename))
        {
            ReadWorksheet(workbook, "Weight_three", "W_THREE", W_three);
            ReadWorksheet(workbook, "Weight_five", "W_FIVE", W_five);

            ReadWorksheet(workbook, "Weight_three", "THREE_WCOMBO", Three_Combo);
            ReadWorksheet(workbook, "Weight_five", "FIVE_WCOMBO", Five_Combo);

            ReadWorksheet(workbook, "Three_Turns", "THREE_WINLOSS", W_threeWL);
            ReadWorksheet(workbook, "Five_Turns", "FIVE_WINLOSS", W_fiveWL);

            ReadWorksheet(workbook, "Three_Turns", "Multiplier_three", M_three);
            ReadWorksheet(workbook, "Five_Turns", "Multiplier_five", M_five);
           
        }
    }

    private void ReadWorksheet(XLWorkbook workbook, string sheetName, string RangeName, List<int> list)
    {
        var worksheet = workbook.Worksheet(sheetName);
        var range = worksheet.Range(RangeName);
        foreach (var cell in range.Cells())
        {
            list.Add((int)cell.Value);
        }
    }

    private void ReadWorksheet(XLWorkbook workbook, string sheetName, string RangeName, List<List<int>> list)
    {
        var worksheet = workbook.Worksheet(sheetName);
        var range = worksheet.Range(RangeName);

        foreach (var row in range.RowsUsed())
        {
            var newRow = new List<int>();
            foreach (var cell in row.CellsUsed())
            {
                newRow.Add((int)cell.Value);
            }
            list.Add(newRow);
        }
    }

    private void ReadWorksheet(XLWorkbook workbook, string sheetName, string RangeName, List<double> list)
    {
        var worksheet = workbook.Worksheet(sheetName);
        var range = worksheet.Range(RangeName);
        foreach (var cell in range.Cells())
        {
            list.Add((double)cell.Value);
        }
    }
}
