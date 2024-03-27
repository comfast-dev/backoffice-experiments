using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using UiTests.Lib.Infra;

namespace UiTests.Lib.Comfast;

public class CfLocator {
    private readonly string _cssXpathChain;
    private readonly string? _description;
    
    public CfLocator(string cssXpathChain, string? description = null) {
        _cssXpathChain = cssXpathChain;
        _description = description;
    }

    public string Text => Find().Text;
    public bool IsDisplayed => (TryFind()?.Displayed) ?? false;

    public bool HasClass(string cssClass) {
        return Find().GetAttribute("class").Split(" ").Contains(cssClass);
    }

    public string[] Texts => Map(el => el.Text).ToArray();

    public void Click() {
        Find().Click();
    }
    
    public void Fill(string text) {
        var el = Find();
        el.Clear();
        el.SendKeys(text);
        Assert.AreEqual(text, el.GetAttribute("value"));
    }

    public string GetAttribute(string name) {
        return Find().GetAttribute(name);
    }
    
    public void ShouldAppear(int? timeoutMs = null) {
        WaitUtils.WaitFor(() => IsDisplayed, "appear element: " + _description, timeoutMs);
    }
    
    public void ShouldDisappear(int? timeoutMs = null) {
        WaitUtils.WaitFor(() => !IsDisplayed,"disappear element: " + _description, timeoutMs);
    }

    
    public IWebElement Find() {
        try {
            return DriverSource.Driver.FindElement(GetBy());
        } catch (NoSuchElementException e) {
            throw new NoSuchElementException($"Not found element:\n{_description} => {_cssXpathChain}\n{CustomErrorInfo()}");
        }
    }

    public IWebElement? TryFind() {
        try {
            return DriverSource.Driver.FindElement(GetBy());
        } catch (NoSuchElementException e) {
            return null;
        }
    }

    public ReadOnlyCollection<IWebElement> FindAll() {
        var driver = DriverSource.Driver;
        return driver.FindElements(GetBy());
    }

    public List<T> Map<T>(Func<IWebElement, T> func) {
        var elements = FindAll().ToList();

        var res = new List<T>();
        foreach (var element in elements) {
            try {
                res.Add(func.Invoke(element));
            } catch (Exception e) {
                var elementHtml = element.GetAttribute("outerHTML").LimitString(50);
                throw new Exception("Mapping failed during processing element [3/45]: " + element.GetAttribute("outerHTML"), e);
            }
        }

        return res;
    }

    protected virtual string CustomErrorInfo() => "";
    
    private By GetBy() {
        bool isXpath = Regex.IsMatch(_cssXpathChain, "[\\.\\(]*/");
        
        return isXpath 
            ? By.XPath(_cssXpathChain) 
            : By.CssSelector(_cssXpathChain);
    }
}