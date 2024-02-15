namespace e2e_tests.Infra;

/**
 * Todo extract values from JSON / Env variables
 */
public class Config {
    public readonly bool Reconnect = true;
    public readonly bool AutoClose = false;
    public readonly string BrowserPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
    public readonly string DriverPath = @"D:\my\bin\chromedriver\121";
}