namespace UiTests.Pages.Backoffice.Components;

public class InputWithSelect(string label) : LabeledComponent(
        $"LOL", $"LOL: '{label}'"), 
    IAnyInput {
    public override string[] AllLabels { get; }
    public void Fill(string value) {
        throw new NotImplementedException();
    }
}