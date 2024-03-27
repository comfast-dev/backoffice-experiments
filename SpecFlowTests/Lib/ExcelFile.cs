using SwiftExcel;
using TechTalk.SpecFlow;

namespace UiTests.Lib;

public class ExcelFile
{
    public readonly string Path;

    /**
     * Get Gherkin table and save it in Excel file
     */
    public ExcelFile(string fileName, Table table) {
        Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "bdd_tests", fileName);

        using (var ew = new ExcelWriter(Path)) {
            var headers = table.Header.ToList();
            for (var col = 0; col < table.Header.Count; col++)
                ew.Write(headers[col], col + 1, 1);

            for (var row = 0; row < table.Rows.Count; row++) {
                var rowData = table.Rows[row].Values.ToList();

                for (var col = 0; col < rowData.Count; col++)
                    ew.Write(rowData[col], col + 1, row + 2);
            }
        }
    }
}