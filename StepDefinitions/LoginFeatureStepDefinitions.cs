using EmpApp.Pages;
using OpenQA.Selenium;

namespace EmpApp.StepDefinitions
{
    [Binding]
    public sealed class LoginFeatureStepDefinitions
    {
        private IWebDriver driver;
        HomePage homePage;
        LoginPage loginPage;

        public LoginFeatureStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
        }

        [Given("I navigate to the URL")]
        public void GivenINavigateToTheURL()
        {
            driver.Url = "http://eaapp.somee.com/";
            Thread.Sleep(1000);
        }

        [When("I click the Login link")]
        public void WhenIClickTheLoginLink()
        {
            homePage.ClickLoginLink();

        }

        [Then("I Enter the credentials as (.*) and (.*)")]
        public void ThenIEnterTheCredentialsAsAdminAndPassword(string username, string password)
        {
            loginPage.Login(username, password);
        }

        [Then("I click the login button")]
        public void ThenIClickTheLoginButton()
        {
            loginPage.ClickLogin();
        }

        [Then("I validate that the admin was logged in successfully")]
        public void ThenIValidateThatTheAdminWasLoggedInSuccessfully()
        {
            homePage.validateHelloAdminButton();
        }

        [Then("I validate that the user was not logged in")]
        public void ThenIValidateThatTheUserWasNotLoggedIn()
        {
            loginPage.validateLoginErrorMessage();
        }

    }
}
