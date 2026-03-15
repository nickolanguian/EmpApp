using EmpApp.Pages;
using OpenQA.Selenium;

namespace EmpApp.StepDefinitions
{
    [Binding]
    public class EmployeeFeatureStepDefinitions
    {
        private IWebDriver driver;
        HomePage homePage;
        EmployeesPage employeesPage;
        CreateEmployeePage createEmployeePage;
        public EmployeeFeatureStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            homePage = new HomePage(driver);
            employeesPage = new EmployeesPage(driver);
            createEmployeePage = new CreateEmployeePage(driver);
        }

        [Then("I click the Employees Link")]
        public void ThenIClickTheEmployeesLink()
        {
            homePage.ClickEmployeesLink();
        }

        [Then("I click the New Employees button")]
        public void ThenIClickTheNewEmployeesButton()
        {
            employeesPage.ClickNewEmployeeButton();
        }

        [Then("I create an Employee using the following details:")]
        public void ThenICreateAnEmployeeUsingTheFollowingDetails(DataTable dataTable)
        {
            dataTable.CreateSet<EmployeeDetails>().ToList().ForEach(employee =>
            {
                createEmployeePage.CreateEmployee(employee.FullName,
                    employee.Age,
                    employee.MonthlySalary,
                    employee.DurationWorked,
                    employee.Grade,
                    employee.EmailAddress);
                Thread.Sleep(3000);
            });
        }

    }

    public class EmployeeDetails
    {
        public string FullName { get; set; }
        public string Age { get; set; }
        public string MonthlySalary { get; set; }
        public string DurationWorked { get; set; }
        public string Grade { get; set; }
        public string EmailAddress { get; set; }
    }
}
