using System;
using UiTests.Lib;
using UiTests.Lib.Comfast;

namespace UiTests.Pages.Backoffice.Components;

public class Checkbox(string label) : LabeledComponent(
        $"//label[contains(.,{Xpath.EscapeText(label)})]/input[@type='checkbox']", $"Checkbox: '{label}'"), 
    IAnyInput {
    
    public override string[] AllLabels => new CfLocator("//label[./input[@type='checkbox']]").Texts;
    
    public void Fill(string value) {
        if (!bool.TryParse(value, out bool result)) {
            throw new Exception($"Invalid value '{value}' for Checkbox {label}. Accept only true/false" );
        }
        
        throw new NotImplementedException();
    }
}