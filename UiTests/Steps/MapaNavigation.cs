using FluentAssertions;

namespace UiTests.Steps;

public class MapaNavigation
{
    /**
     * Log in if needed
     */
    public void LogIn(string user, string pass) {
        Open("https://onboarding.direct.preprod.worldline-solutions.com/mapa/Account/Login");
        if (GetWebDriver().Url.Contains("Account/Login")) DoLogIn(user, pass);
    }

    private void DoLogIn(string user, string pass) {
        S("#UserName").Set(user);
        S("#Password").Set(pass);
        S("#btn_Login").Click();

        S("#lnk_LogOff").Should(Be.Visible);
    }

    public void NavigateToMenu(string menuName) {
        NavigateToLink(S($"nav.sidebar a[title='{menuName}']"));
    }

    public void NavigateToLink(SeleneElement link) {
        var href = link.GetAttribute("href");

        link.Click();
        href.Should().Equals(Url());
    }
}