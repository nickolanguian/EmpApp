using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using EmpApp.Utility;

namespace EmpApp.Pages
{
    public class HomePage
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement btnLogin => driver.FindElement(By.LinkText("Login"));
        IWebElement formLogin => driver.FindElement(By.CssSelector(".login-body"));
        IWebElement btnHelloAdmin => driver.FindElement(By.LinkText("Hello admin!"));
        IWebElement btnEmployees => driver.FindElement(By.PartialLinkText("Employees"));
        IWebElement lblPageTitle => driver.FindElement(By.CssSelector(".page-title"));


        public void ClickLoginLink()
        {
            btnLogin.ClickElement();
            Assert.That(formLogin.Displayed, Is.True, "Login form is not displayed");

        }
        public void validateHelloAdminButton()
        {
            Assert.That(btnHelloAdmin.Displayed, Is.True, "Admin button is not displayed");
        }
        public void ClickEmployeesLink()
        {
            btnEmployees.ClickElement();
            StringAssert.Contains("Employees", lblPageTitle.Text, "Page title is not as expected");

        }
    }
}
