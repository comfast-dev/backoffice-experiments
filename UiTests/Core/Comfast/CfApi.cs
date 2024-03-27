using OpenQA.Selenium;
using UiTests.Lib.Infra;

namespace UiTests.Lib.Comfast;

public static class CfApi {
    private static IWebDriver GetDriver() => DriverSource.Driver;
    public static string CurrentUrl => GetDriver().Url;

    public static void NavigateTo(string url) {
        DriverSource.Driver.Navigate().GoToUrl(url);
    }
}