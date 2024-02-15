using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace UiTests.Lib.Infra;

public static class DriverSessionStore
{
    private const string Separator = "#";
    private static readonly TempFile SessionTempFile = new("WebDriverSessionInfo.txt");

    // ReSharper disable once MemberCanBePrivate.Global
    public static void StoreSessionInfo(WebDriver driver) {
        var ssid = Utils.readField<string>(driver, "SessionId.sessionOpaqueKey");
        var uri = Utils.readField<string>(driver, "CommandExecutor.HttpExecutor.remoteServerUri.AbsoluteUri");
        SessionTempFile.save($"{uri}{Separator}{ssid}");
    }

    /// <summary>
    /// Tries to restore WebDriver session..
    /// It is only possible when:
    /// 1. driver process still running e.g. chromedriver.exe
    /// 2. browser still running
    /// 3. Data is stored - called method <see cref="StoreSessionInfo"/>
    /// </summary>
    /// <param name="newDriverProvider">WebDriver provider in case of restore fail</param>
    /// <returns></returns>
    public static WebDriver RestoreSessionOrElse(Func<WebDriver> newDriverProvider) {
        try {
            var sessionInfo = SessionTempFile.read().Split(Separator);
            var recreatedDriver = new RemoteWebDriver(
                new FixedSessionExecutor(sessionInfo[0], sessionInfo[1]),
                new RemoteSessionSettings());

            recreatedDriver.FindElements(By.CssSelector("html"));
            return recreatedDriver;
        }
        catch (Exception e) {
            var newDriver = newDriverProvider.Invoke();
            StoreSessionInfo(newDriver);
            return newDriver;
        }
    }
}

internal class FixedSessionExecutor : HttpCommandExecutor
{
    private readonly string _sessionId;

    public FixedSessionExecutor(string uri, string sessionId) : base(new Uri(uri), TimeSpan.FromSeconds(60)) {
        _sessionId = sessionId;
    }

    public override Response Execute(Command command) {
        return command.Name == DriverCommand.NewSession
            ? MockNewSession()
            : base.Execute(command);
    }

    private Response MockNewSession() {
        var response = new Response {
            SessionId = _sessionId,
            Status = WebDriverResult.Success,
            Value = new Dictionary<string, object>()
        };
        return response;
    }
}