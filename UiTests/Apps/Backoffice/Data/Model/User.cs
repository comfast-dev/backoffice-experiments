namespace UiTests.Data.Model;

public class User(string username, string password) {
    public string Password => password;
    public string Username => username;
}