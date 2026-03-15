using EmpApp.Utility;
using OpenQA.Selenium;

namespace EmpApp.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement txtUserName => driver.FindElement(By.Id("UserName"));
        IWebElement txtPassword => driver.FindElement(By.Id("Password"));
        IWebElement btnSignIn => driver.FindElement(By.CssSelector(".btn"));
        IWebElement lblLoginError => driver.FindElement(By.CssSelector(".validation-summary-errors"));

        public void Login(string username, string password)
        {
            txtUserName.EnterText(username);
            txtPassword.EnterText(password);
        }
        public void ClickLogin()
        {
            btnSignIn.SubmitElement();
        }

        public void validateLoginErrorMessage()
        {
            if (lblLoginError.Displayed)
            {
                Console.WriteLine("Login failed with error: " + lblLoginError.Text);
            }
            else
            {
                Console.WriteLine("No login error message displayed.");
            }
        }
    }
}
