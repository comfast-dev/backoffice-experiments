
using NSelene;

namespace UiTests.Steps;

public class MapaNavigation
{
    public void LogIn(string user, string pass) {
        Selene.Open("https://onboarding.direct.preprod.worldline-solutions.com/mapa/Account/Login");
        if (Selene.GetWebDriver().Url.Contains("Account/Login")) DoLogIn(user, pass);
    }

    private void DoLogIn(string user, string pass) {
        Selene.S("#UserName").Set(user);
        Selene.S("#Password").Set(pass);
        Selene.S("#btn_Login").Click();

        Selene.S("#lnk_LogOff").Should(Be.Visible);
    }

    public void NavigateToMenu(string menuName) {
        NavigateToLink(Selene.S($"nav.sidebar a[title='{menuName}']"));
    }

    public void NavigateToLink(SeleneElement link) {
        var href = link.GetAttribute("href");

        link.Click();
        
        // href.Should().Equals(Url());
    }
}