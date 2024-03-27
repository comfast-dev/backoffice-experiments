
using UiTests.Lib.Comfast;

namespace UiTests.Steps;

public class MapaApp {
    private const string LoginUrl = "Account/Login";
    
    private readonly MapaConfig _config;
    public MapaApp(MapaConfig config) {
        _config = config;
    }
    
    public void LogIn(string user, string pass) {
        CfApi.NavigateTo(_config.BaseUrl);
        if(CfApi.CurrentUrl.Contains(LoginUrl)) DoLogIn(user, pass);
    }

    private void DoLogIn(string user, string pass) {
        new CfLocator("#UserName").Fill(user);
        new CfLocator("#Password").Fill(pass);
        new CfLocator("#btn_Login").Click();

        new CfLocator("#lnk_LogOff", "Logout link").ShouldAppear();
    }

    public void NavigateToMenu(string menuName) {
        NavigateToLink(new CfLocator($"nav.sidebar a[title='{menuName}']"));
    }

    public void NavigateToLink(CfLocator link) {
        var href = link.GetAttribute("href");

        link.Click();
        
        // href.Should().Equals(Url());
    }
}