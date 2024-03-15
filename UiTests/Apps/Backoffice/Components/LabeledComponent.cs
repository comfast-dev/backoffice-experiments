using UiTests.Lib;
using UiTests.Lib.Comfast;

namespace UiTests.Pages.Backoffice.Components;

public abstract class LabeledComponent(string cssOrXpath, string description) : CfLocator(cssOrXpath, description) {
    
    protected override string CustomErrorInfo() {
        var labels = string.Join("\", \"", AllLabels);
        return $"Available {GetType().Name} labels are: \"{labels}\" \n";
    }

    public abstract string[] AllLabels { get; }
    
    public string RecognizeElements() {
        return string.Join("\n", AllLabels.Where(l => l.Trim().Length > 0)
            .Where(l => {
                var el = (LabeledComponent)Activator.CreateInstance(GetType(), l);
                return el.IsDisplayed;
            })
            .Select(label => {
                var fieldName = label
                    .RgxReplace(" +", "_")
                    .RgxReplace("[^A-Za-z]", "")
                    .TrimLength(20);
                return $"{GetType().Name} {fieldName} = new(\"{label}\");";
            }));
    }
}