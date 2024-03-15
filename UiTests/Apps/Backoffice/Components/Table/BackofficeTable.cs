using UiTests.Lib.Comfast;

namespace UiTests.Apps.Backoffice.Components.Table;

public class BackofficeTable {
    public readonly CfLocator NoItemsToDisplay = new("//*[@id='checklistcontainer']//span[.='No items to display']");
    
    public CfLocator FindCell(string columnName, string cellText) {
        if (NoItemsToDisplay.IsDisplayed) throw new Exception(
            $"Can't find cell: {columnName} -> {cellText}. Table is empty");
        
        var columns = GetAllColumns();
        int columnIndex = Array.FindIndex(columns, s => s.Trim().Equals(columnName));
        var cells = new CfLocator($"//*[@id='ChecklistGrid']//tbody/tr/td[{columnIndex+1}]").Texts;
        
        int rowIndex = Array.FindIndex(cells, c => c.Trim().Equals(cellText));
        
        return new CfLocator($"//*[@id='ChecklistGrid']//tbody/tr[{rowIndex+1}]/td[{columnIndex+1}]");
    }

    public string[] GetAllColumns() {
        return new CfLocator("//*[@id='ChecklistGrid']//th").Texts;
    }
}