using UiTests.Apps.Backoffice.Components.Table;
using UiTests.Data;
using UiTests.Data.Model;
using UiTests.Lib.Comfast;
using UiTests.Pages.Backoffice.Components;
using UiTests.Pages.Backoffice.Components.Menu;

namespace UiTests.Pages.Backoffice;

public class BackofficeApp {
    private readonly BoEnvData _config;
    public readonly FormService FormService = new();
    public readonly LoginPage LoginPage;

    public BackofficeApp(Env env) {
        _config = ConfigData.getConfig(env);
        LoginPage = new LoginPage(_config);
    }

    public void LogIn() {
        var user = _config.AbsysUser;
        LoginPage.LogInAs(user.Username, user.Password, UserType.AbsysUser);
    }
    
    public void LogInAs(string user, string password) {
        LoginPage.LogInAs(user, password, UserType.AbsysUser);
    }

    public void NavigateTo(string menuLvl1, string? menuLvl2 = null, string? menuLvl3 = null, string? menuLvl4 = null) {
        new MenuLvl1(menuLvl1).Open();
        if (menuLvl3 != null) new MenuLvl2(menuLvl2).Open();
        if (menuLvl3 != null) new MenuLvl3(menuLvl3).Open();
        if (menuLvl4 != null) new MenuLvl4(menuLvl4).Open();
        waitForLoadEnd();
    }

    public FormService FillForm(Dictionary<string, string> data) {
        return new FormService().fillForm(data);
    }

    public void waitForLoadEnd(int minimumWait = 0) {
        Thread.Sleep(minimumWait);
        CfLocator anyLoading = new("#div-reload-filters .k-loading, .k-loading-mask", "Any loading indicator");
        
        anyLoading.ShouldDisappear(20000);
    }

    public BackofficeTable table() {
        return new BackofficeTable();
    }
}

public class FormService {
    public FormService fillForm(Dictionary<string, string> data) {
        foreach (var entry in data) {
            var label = entry.Key;
            var value = entry.Value;
            
            var anyInput = recognizeInputType(label);
            
            try {
                anyInput.Fill(value);
            } catch (Exception e) {
                throw new Exception($"Failed to fill input {anyInput.GetType().Name}{label} => {value}", e);
            }
        }

        return this;
    }

    public void SubmitForm() {
        new Submit().Click();
    }

    private Input recognizeInputType(string label) {
        return new Input(label);
    }
}