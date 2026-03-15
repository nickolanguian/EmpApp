using EmpApp.Utility;
using NUnit.Framework;
using OpenQA.Selenium;

namespace EmpApp.Pages
{
    public class CreateEmployeePage
    {
        private IWebDriver driver;
        public CreateEmployeePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        IWebElement lblCreateNewEmployee => driver.FindElement(By.XPath("//h2[text()='Create New Employee']"));
        IWebElement txtFullName => driver.FindElement(By.Id("Name"));
        IWebElement txtAge => driver.FindElement(By.Id("Age"));
        IWebElement txtMonthlySalary => driver.FindElement(By.Id("Salary"));
        IWebElement txtDurationWorked => driver.FindElement(By.Id("DurationWorked"));
        IWebElement selectGrade => driver.FindElement(By.Id("Grade"));
        IWebElement txtEmailAddress => driver.FindElement(By.Id("Email"));
        IWebElement btnCreateEmployee => driver.FindElement(By.XPath("//button[contains(text(),'Create Employee')]"));
        IWebElement btnCreateEmployee2 => driver.FindElement(By.XPath("//button[contains(text(),'Create Employee the 2nd')]"));

        public void CreateEmployee(string fullName, string age, string monthlySalary, string durationWorked, string grade, string emailAddress)
        {
            Assert.That(lblCreateNewEmployee.Displayed, Is.True);
            if (lblCreateNewEmployee.Displayed)
            {
                txtFullName.EnterText(fullName);
                txtAge.EnterText(age);
                txtMonthlySalary.EnterText(monthlySalary);
                txtDurationWorked.EnterText(durationWorked);
                selectGrade.SelectDropdownByText(grade);
                txtEmailAddress.EnterText(emailAddress);
                btnCreateEmployee.Click();
            }
        }

    }
}
