using e2e_tests.Infra;

namespace e2e_tests.Hooks;

[Binding]
public class InitHooks {
    [BeforeTestRun]
    public void InitDriver() {
        Configuration.Driver = DriverSource.Driver;
    }
}