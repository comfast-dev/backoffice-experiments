@mapa
Feature: [og-mapa] User Migration Waves


Background:
  Given I am logged into Mapa
  And I navigate to "User Migration Waves" page


@SMBO-121018
Scenario Outline: TC1: Upload incorrectly structured excel file
  When I Click Upload Wave with file: "<filename>"
  Then Mapa <place of error> error should be shown: "<error>"

  Examples:
    | filename | error                                            | place of error |
    | TC1.1    | Column layout is not valid : Column with index=0 | upload page    |
    | TC1.3    | Column layout is not valid : Column with index=1 | upload page    |
    | TC1.2    | File does not contain any data row               | result page    |
    | TC1.4    | File NOT valid                                   | result page    |


@SMBO-121020
Scenario Outline: TC2: Upload correctly structured excel file
  When I Click Upload Wave with file: "<filename>"
  Then Upload results should show <count> results with status OK
  When I click Process button
  Then Migration details should show <count> results with status "New"
  When I refresh the list (click finished)
  #Status: ???
  Then Migration details should show <count> results with status "Valid and Finished"

  Examples:
    | filename | count |
    | TC2.1    | 6     |
    | TC2.2    | 2     |


@SMBO-126722
Scenario: TC3.1: PSPID from ISP that is not mapped in og-account
  Given INFO: PayPage ISP is mapped in og-account
  And INFO: PFIN ISP is not mapped in og-account
  When I Click Upload Wave with file: "TC3.1"
  Then Upload results should show 2 results with status OK
  When I click Process button
  Then Migration details should show 2 results with status "New"
  When I refresh the list (click finished)
  #Status: FinishedWithWarnings
  Then Migration details should show
    | PSPID                  | PSP Status | User Migration Status |
    | mapaE2EMerchantPayPage | Valid      | Finished              |
    | mapaE2EPostFinance     | Valid      | Failed                |


@SMBO-126725
Scenario: TC3.2: Merchant group instead of PSPID
  When I Click Upload Wave with file: "TC3.2"
  Then Upload results should show 1 entries with status OK
  When I click Process and refresh list
  Then Migration details status should be "FinishedWithErrors" and list:
    | PSPID           | PSP Status |
    | mapaE2EMerchGrp | Invalid    |


@SMBO-126726
Scenario: TC3.3: Merchant with API user
  Given INFO: Merchant with 2 admin users and 1 API user
  When I Click Upload Wave with file: "TC3.2"
  Then Upload results should show 1 entries with status OK
  When I click Process and refresh list
  Then Migration details status should be "FinishedWithErrors" and list:
    | PSPID              | PSP Status | UM Status |
    | mapaE2EMerchantOBE | Invalid    | Finished  |
    | SOME_API_USER      | Invalid    | Finished  |


#//todo


@SMBO-126730
Scenario: TC3.4: Subscribed and inactive merchants
  When I Click Upload Wave with file: "TC3.4"
  Then Upload results should show 1 entries with status OK
  When I click Process and refresh list
  Then Migration details status should be "FinishedWithErrors" and list:
    | PSPID                        | PSP Status |
    | mapaE2EMerchantOITSubscribed | Invalid    |
    | mapaE2EMerchantOITInactive   | Invalid    |


@SMBO-126733
Scenario: TC3.5: Active merchants
  When I Click Upload Wave with file: "TC3.5"
  Then Upload results should show 4 entries with status OK
  When I click Process and refresh list
  Then Migration details should show 5 results with status "Valid and Finished"
#  Then Migration details status should be "FinishedWithErrors" and list:
#    | PSPID                    | PSP Status | Description                                                         |
#    | mapaE2EMerchantOITActive | PSP Status | Active merchant with 2 active admin users and 1 inactive admin user |
#    | mapaE2EMerchantOCH       | Invalid    | Active merchant in OCH ISP                                          |
#    | mapaE2EMerchantODE       | Invalid    | Active merchant in ODE ISP                                          |
#    | mapaE2EMerchantOFR       | Invalid    | Active merchant in OFR ISP                                          |


@SMBO-126735
Scenario: TC3.6: Active merchant with multiple users
  When I Click Upload Wave with file: "TC3.6"
  Then Upload results should show 8 entries with status OK
  And Merchant is in Active status
  And All users are in Active status

  When I click Process and refresh list
  Then Migration details should show 8 results with status "Valid and Finished"


@SMBO-126813
Scenario: TC3.6: Validate in User Manager
  Given previous test @SMBO-126735 is done
  Given I log in User Manager Application as Admin
  When I navigate Manage Team
  And I select "mapaE2EMerchantOITIsBigestName" account

  Then There should be 6 entries in User Manager