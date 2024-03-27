namespace UiTests.Lib.Comfast;

public class DriverConfig {
    public static string BrowserName = "chrome";
    public static int TimeoutMs = 10000;
    public readonly bool Reconnect = true;
    public readonly bool AutoClose = false;
    public readonly string BrowserPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
    public readonly string DriverPath = @"D:\my\bin\chromedriver\121";
}