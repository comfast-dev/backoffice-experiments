using UiTests.Lib;
using UiTests.Lib.Comfast;

namespace UiTests.Pages.Backoffice.Components;

public class Select(string label) : LabeledComponent(
    $"//label[.={Xpath.EscapeText(label)}]/../following-sibling::*//select", $"Input: '{label}'"), 
    IAnyInput {
    public override string[] AllLabels => new CfLocator("//label[../following-sibling::*//select]").Texts;
    
    public void Fill(string value) {
        throw new NotImplementedException();
    }
}