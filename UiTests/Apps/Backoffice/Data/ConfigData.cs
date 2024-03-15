using UiTests.Data.Model;

namespace UiTests.Data;

public class ConfigData
{
    static readonly Dictionary<Env, BoEnvData> _backofficeData = new() {
        { Env.Dev, new BoEnvData { BaseUrl = "not yet", AbsysUser = new User("", "") } },
        { Env.Int, new BoEnvData { BaseUrl = "not yet", AbsysUser = new User("", "") } },
        { Env.Test, new BoEnvData { BaseUrl = "https://secure.ogone.com/Ncol/Test/Backoffice", AbsysUser = new User("PUT_TRIGRAM_HERE", "PUT_PASSWORD_HERE") } },
        { Env.Prod, new BoEnvData { BaseUrl = "not yet", AbsysUser = new User("", "") } },
    };

    public static BoEnvData getConfig(Env env) {
        return _backofficeData[env];
    }
}

public class BoEnvData
{
    public string BaseUrl { get; set; }
    public User AbsysUser { get; set; }
}