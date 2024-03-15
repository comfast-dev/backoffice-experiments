using System.Text;
using UiTests.Lib;
using UiTests.Lib.Comfast;

namespace UiTests.Pages.Backoffice.Components;

public class LinkEl(string label) : LabeledComponent(
    $"//a[{Xpath.TextIs(label)}]", 
    $"Link text: '{label}'"
    ) {
    
    public override string[] AllLabels => new CfLocator("//a").Texts;
}