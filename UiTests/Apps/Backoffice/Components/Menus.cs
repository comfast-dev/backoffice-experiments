using OpenQA.Selenium.DevTools.V85.Input;
using UiTests.Lib;
using UiTests.Lib.Comfast;

namespace UiTests.Pages.Backoffice.Components.Menu;

public abstract class BaseMenu(string cssOrXpath, string description) : LabeledComponent(cssOrXpath, description) {
    public abstract bool IsOpen { get; }

    public void Open() {
        if (IsOpen) return;
        Click();
        WaitUtils.WaitFor(() => IsOpen, $"Open menu item: {this}", 10000);
        waitForAllEntriesLoad();
    }

    private void waitForAllEntriesLoad() {
        // Thread.Sleep(1000);
        CfLocator anyLoading = new("#div-reload-filters .k-loading, .k-loading-mask", "Any loading indicator");
        anyLoading.ShouldDisappear(20000);
    }
}

public class MenuLvl1(string label) : BaseMenu(
    BaseXpath + $"[./a='{label}']", $"Menu1 item: '{label}'") {
    
    public override string[] AllLabels => new CfLocator(BaseXpath + "/a/span").Texts;
    
    private const string BaseXpath = "//*[@id='headernavigation']/ul/li";

    public override bool IsOpen => HasClass("selected") || HasClass("select");
}

public class MenuLvl2(string label) : BaseMenu(BaseXpath + $"[.='{label}']", $"Menu2 item: '{label}'") {
    public override string[] AllLabels => new CfLocator(BaseXpath + "/span").Texts;

    private const string BaseXpath = "//div[@id='submenu']//ul[@class='cvrmenu']/li/a";

    public override bool IsOpen => HasClass("linkselected");
}

public class MenuLvl3(string label) : BaseMenu(BaseXpath + $"[./a='{label}']", $"Menu3 item: '{label}'") {
    public override string[] AllLabels => new CfLocator(BaseXpath + "/a").Texts;

    private const string BaseXpath = "//ul[@id='maintab']/li";

    public override bool IsOpen => HasClass("selected");
}

public class MenuLvl4(string label) : BaseMenu(BaseXpath + $"[normalize-space(./a)='{label}']", $"Menu4 item: '{label}'") {
    public override string[] AllLabels => new CfLocator(BaseXpath + "/a").Texts;

    private const string BaseXpath = "//div[contains(@class,'link-small-method')]/ul/li[contains(@class, 'method-item')]";

    public override bool IsOpen => HasClass("method-item-active");
}