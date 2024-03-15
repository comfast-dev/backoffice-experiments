using UiTests.Data;
using UiTests.Data.Model;
using UiTests.Lib.Comfast;
using UiTests.Pages.Backoffice.Components;

namespace UiTests.Pages.Backoffice;

public class LoginPage {
    public const string LoginUrl = "/Ncol/Test/Backoffice/login/index";

    Input UserIdInput = new("User ID");
    Input PSPIDInput = new("PSPID");
    Input PSPIDOptional = new("PSPID (Optional)");
    Input Passwordinput = new("Password");
    Button SubmitBtn = new("Submit");
    LinkEl loginAsUser = new("Login as user");
    LinkEl loginAsPSPID = new("Login as PSPID");
    
    CfLocator FooterStatus = new(".bottom-right > span", "Footer Status");
    CfLocator UserIcon = new("#connectUserInfoBtn", "User Icon");
    LinkEl LogoutLink = new("Logout");
    CfLocator LoginForm = new("//div[@class='login-title bold'][./span/.='Login']", "Login form");
    private CfLocator LoginErrorMsg = new("#errorMessage", "Login Error msg");
    
    private BoEnvData _config;
    public LoginPage(BoEnvData config) {
        _config = config;
    }

    public bool IsLoggedIn => UserIcon.IsDisplayed;
    public bool IsOnLoginPage => CfApi.CurrentUrl.Contains(LoginUrl);
    
    public void LogInAs(string username, string password, UserType userType) {
        if (IsLoggedIn) {
            if (IsLoggedAs(username, userType)) return;
            Logout();
        }
        if (!IsOnLoginPage) CfApi.NavigateTo(_config.BaseUrl);
        GoToFormType(userType);
        
        var usernameInput = userType == UserType.AbsysUser ? UserIdInput : PSPIDInput;
        usernameInput.Fill(username);
        Passwordinput.Fill(password);
        SubmitBtn.Click();

        if (!IsLoggedIn) throw new Exception("Failed to log in with error: " + LoginErrorMsg.Text);
    }
    
    private void Logout() {
        UserIcon.Click();
        LogoutLink.Click();
        LoginForm.ShouldAppear();
    }

    private bool IsLoggedAs(string username, UserType userType) {
        if (!IsLoggedIn) return false;

        return FooterStatus.Text.Contains(userType switch {
            UserType.AbsysUser => $"UserID: {username} -",
            UserType.Pspid => $"| PSPID: {username} |"
        });
    }
    
    private void GoToFormType(UserType userType) {
        if (userType == UserType.AbsysUser && loginAsUser.IsDisplayed) {
            loginAsUser.Click();
            loginAsUser.ShouldDisappear();
        } else if (userType == UserType.Pspid && loginAsPSPID.IsDisplayed) {
            loginAsPSPID.Click();
            loginAsPSPID.ShouldDisappear();
        }
    }
}