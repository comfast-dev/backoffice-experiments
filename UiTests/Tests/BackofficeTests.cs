using NUnit.Framework;
using UiTests.Apps.Backoffice;
using UiTests.Data;
using UiTests.Pages.Backoffice;
using UiTests.Pages.Backoffice.Components;
using UiTests.Pages.Backoffice.Components.Menu;

namespace UiTests.Tests;

public class BackofficeTests
{
    static readonly Dictionary<Env, BackofficeConfig> _backofficeData = new() {
        { Env.Dev, new BackofficeConfig { BaseUrl = "not yet", AbsysUser = new User("", "") } },
        { Env.Int, new BackofficeConfig { BaseUrl = "not yet", AbsysUser = new User("", "") } },
        { Env.Test, new BackofficeConfig { BaseUrl = "https://secure.ogone.com/Ncol/Test/Backoffice", AbsysUser = new User("pku", "aaa") } },
        { Env.Prod, new BackofficeConfig { BaseUrl = "not yet", AbsysUser = new User("", "") } },
    };
    
    private readonly BackofficeApp _backoffice = new(_backofficeData[Env.Test]);

    [Test] public void backoffice_login() {
        _backoffice.LogInAsAbsys();
        
        Console.WriteLine(String.Join("\n", new LabeledInput("").RecognizeElements()));
        Console.WriteLine(String.Join("\n", new LinkEl("").RecognizeElements()));
    }

    [Test] public void aaaa() {
        // Console.WriteLine(String.Join("\n", new Input("").RecognizeElements()));
        Console.WriteLine(String.Join("\n", new MenuLvl2("").RecognizeElements()));
    }

    [Test] public void impersonate() {
        _backoffice.LogInAsAbsys();
        
        _backoffice.NavigateTo("Other merchant");

        var merchant = "pklTestCompany2";
        
        _backoffice.FillForm(new Dictionary<string, string>() {
            { "PSPID", merchant },
        }).SubmitForm();
        
        _backoffice.waitForLoadEnd(1000);

        _backoffice.table()
            .FindCell("PSPID", merchant)
            .Click();
        _backoffice.waitForLoadEnd(1000);
    }

    [Test] public void print_menus() {
        new MenuLvl1("").RecognizeElements();
        new MenuLvl2("").RecognizeElements();
        new MenuLvl3("").RecognizeElements();
        new MenuLvl4("").RecognizeElements();
    }

    [Test]
    public void open_menus() {
        foreach (var label in new MenuLvl1("").AllLabels) {
            new MenuLvl1(label).Open();
            foreach (var label2 in new MenuLvl2("").AllLabels) {
                new MenuLvl2(label2).Open();
                foreach (var label3 in new MenuLvl3("").AllLabels) {
                    new MenuLvl3(label3).Open();
                    foreach (var label4 in new MenuLvl4("").AllLabels) {
                        new MenuLvl4(label4).Open();
                    }
                }
            }
        }
    }
}