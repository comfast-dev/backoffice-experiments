namespace UiTests.Data;

public class BackofficeConfig
{
    public string BaseUrl { get; set; }
    public User AbsysUser { get; set; }
    public User MerchantUser { get; set; }
}

public enum UserType { AbsysUser, Pspid }
public enum Env { Dev, Int, Test, Prod }

public class User(string username, string password) {
    public string Password => password;
    public string Username => username;
}