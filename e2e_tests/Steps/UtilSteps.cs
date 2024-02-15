using e2e_tests.Steps.Util;
using SwiftExcel;

namespace e2e_tests.Steps;

[Binding]
public class UtilSteps {
    
    
    [Given(@"I am logged into Mapa")]
    public void CreateExcelFile() {
        
    }

    [Given(@"Excel file ""(.*)"" with content:")]
    public void GivenExcelFile(string fileName, Table table) {
        new ExcelFile(fileName, table);
    }
}