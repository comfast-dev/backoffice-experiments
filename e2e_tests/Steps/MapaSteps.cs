﻿using e2e_tests.Infra;
using e2e_tests.Steps.Util;
using FluentAssertions;

namespace e2e_tests.Steps;

[Binding]
[Scope(Tag = "mapa")]
public sealed class MapaSteps {
    private MapaNavigation mapaNav = new();
    
    [Given(@"I am logged into Mapa")]
    public void GivenIAmLoggedInMapa() {
        mapaNav.LogIn("pku","lol");
    }

    [Given(@"I navigate to ""(.*)"" page")]
    public void NavigateToPage(string pageName) {
        mapaNav.NavigateToMenu(pageName);
    }
    
    [When(@"I Click Upload Wave with file: ""(.*)""")]
    public void WhenIClickUploadWaveWithFile(string p0) {
        mapaNav.NavigateToLink(S("//a[text()='Upload wave']"));
    }
    
    // [When(@"Click /Upload Wave/ with file: ""(.*)""")]
    // public void WhenClickUploadWaveWithFile(string p0) {
    //     ScenarioContext.StepIsPending();
    // }
    
    [Then("Mapa (upload|result) page error should be shown: \".*\"")]
    public void mapaErrorShouldBeShow(string errorMessage) {
        ScenarioContext.StepIsPending();
    }

    public void mapa_upload_page_error_should_be_shown_STRING(string errorMessage) {
        ScenarioContext.StepIsPending();
    }
    
    [When(@"I Click Upload Wave with file: ""(.*)""")]
    public void WhenClickUploadWaveWithFile(string p0) {
        ScenarioContext.StepIsPending();
    }

    [Then(@"Wait (.*)ms")]
    public void ThenWaitMs(int ms) {
        Thread.Sleep(ms);
    }

    [When(@"I click Upload Wave and attach file: ""(.*)""")]
    public void WhenIClickUploadWaveAndAttachFile(string fileName, Table table) {
        var path = new ExcelFile(fileName, table).path;

        S("input[type=file]").SendKeys(path).Click();
    }
}