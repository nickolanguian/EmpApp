using OpenQA.Selenium;
using EmpApp.Utility;

namespace EmpApp.Pages
{
    public class EmployeesPage
    {
        private IWebDriver driver;
        public EmployeesPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        IWebElement btnNewEmployee => driver.FindElement(By.CssSelector(".btn-create"));
        public void ClickNewEmployeeButton()
        {
            btnNewEmployee.ClickElement();
        }
    }
}
