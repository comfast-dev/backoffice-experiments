using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UiTests.Lib.Comfast;

namespace UiTests.Lib.Infra;

/**
 * Return one Driver instance per thread
 */
public class DriverSource
{
    private static readonly DriverConfig _config = new();
    private static readonly ThreadLocal<WebDriver> Instances = new(ProvideDriverInstance);
    public static WebDriver Driver => GetDriver();

    /**
     * Provide WebDriver Instance.
     * One per thread.
     * Can be called multiple times.
     */
    public static WebDriver GetDriver() {
        return Instances.Value;
    }

    private static WebDriver ProvideDriverInstance() {
        var driver = _config.Reconnect && !_config.AutoClose
            ? DriverSessionStore.RestoreSessionOrElse(runNewDriver)
            : runNewDriver();

        if (_config.AutoClose) addShutdownHook(driver);

        return driver;
    }

    /**
     * Make sure WebDriver is closed after end of process
     */
    private static void addShutdownHook(WebDriver driver) {
        AppDomain.CurrentDomain.ProcessExit += (s, e) => {
            driver.Close(); // closes browser
            driver.Dispose(); // closes driver process ( e.g. chromedriver.exe ) 
        };
    }

    /**
     * Run new WebDriver instance
     */
    private static ChromeDriver runNewDriver() {
        return new ChromeDriver(_config.DriverPath, new ChromeOptions {
            BinaryLocation = _config.BrowserPath
        });
    }
}