using UiTests.Lib.Infra;

namespace UiTests.Lib;

[Binding]
public class InitHooks
{
    [BeforeTestRun]
    public static void InitDriver() {
        Configuration.Driver = DriverSource.Driver;
    }
}