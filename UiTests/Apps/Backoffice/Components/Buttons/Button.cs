using UiTests.Lib;
using UiTests.Lib.Comfast;

namespace UiTests.Pages.Backoffice.Components;

public class Button(string label) : LabeledComponent(
    BaseXpath + $"[contains(., '{label}')]", 
    $"Button: '{label}'") {
    
    public override string[] AllLabels => new CfLocator(
        BaseXpath + "/span[string-length(normalize-space()) > 0]").Texts;

    private const string BaseXpath = "(//button|//a[contains(@class,'button')])";
}