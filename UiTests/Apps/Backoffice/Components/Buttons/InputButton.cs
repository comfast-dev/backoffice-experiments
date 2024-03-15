using UiTests.Lib;
using UiTests.Lib.Comfast;

namespace UiTests.Pages.Backoffice.Components;

public class InputButton(string label) : LabeledComponent(
    BaseXpath + $"[@value='{label}']", $"Button: '{label}'") {
    
    public override string[] AllLabels => new CfLocator(BaseXpath)
        .Map(el => el.GetAttribute("value"))
        .ToArray();

    private const string BaseXpath = "//input[@type='button' or @type='submit']";
}
