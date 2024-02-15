﻿using UiTests.Lib;

namespace UiTests.Steps;

[Binding]
public class UtilSteps
{
    [Given(@"Excel file ""(.*)"" with content:")]
    public void GivenExcelFile(string fileName, Table table) {
        new ExcelFile(fileName, table);
    }
}