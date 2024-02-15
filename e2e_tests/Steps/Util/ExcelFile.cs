using SwiftExcel;

namespace e2e_tests.Steps.Util;

public class ExcelFile {
    public readonly string path;
    
    public ExcelFile(string name, Table table) {
        path = Path.Combine(Path.GetTempPath(), "bdd_tests", name);
        
        using (var ew = new ExcelWriter(path)) {
            var headers = table.Header.ToList();
            for (var col = 0; col < table.Header.Count; col++)
                ew.Write(headers[col], col + 1, 1);
            
            for (var row = 0; row < table.Rows.Count; row++) {
                var rowData = table.Rows[row].Values.ToList();
            
                for (var col = 0; col < rowData.Count; col++) 
                    ew.Write(rowData[col], col+1, row + 2);
            }
        }
    }
}