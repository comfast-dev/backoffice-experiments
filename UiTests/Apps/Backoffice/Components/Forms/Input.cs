using NUnit.Framework;
using UiTests.Lib;
using UiTests.Lib.Comfast;

namespace UiTests.Pages.Backoffice.Components;

public class Input(string label) : LabeledComponent(
    $"//label[.={Xpath.EscapeText(label)}]/../following-sibling::*//input", $"Input: '{label}'"), 
    IAnyInput {
    
    public override string[] AllLabels => new CfLocator("//label[../following-sibling::*//input]").Texts;

    public void Fill(string text) {
        var el = Find();
        el.Clear();
        el.SendKeys(text);
        Assert.AreEqual(text, el.GetAttribute("value"));
    }
}