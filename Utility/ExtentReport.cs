using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;

namespace EmpApp.Utility
{
    public class ExtentReport
    {
        public static ExtentReports _extentReports;
        public static ExtentTest _feature;
        public static ExtentTest _scenario;

        public static String dir = AppDomain.CurrentDomain.BaseDirectory;
        public static String folderName = "Reports_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
        public static String testResultPath = dir.Replace("bin\\Debug\\net10.0", "TestResults\\" + folderName);
        public static String lastRunPath = dir.Replace("bin\\Debug\\net10.0", "TestResults\\LastRunReport");

        public static void ExtentReportInit()
        {
            Directory.CreateDirectory(testResultPath);
            var htmlReporter = new ExtentHtmlReporter(testResultPath);
            htmlReporter.Config.ReportName = "Automation Test Report";
            htmlReporter.Config.DocumentTitle = "Test Execution Report";
            htmlReporter.Config.Theme = Theme.Standard;
            htmlReporter.Start();

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(htmlReporter);
            _extentReports.AddSystemInfo("Application", "EAPP");
            _extentReports.AddSystemInfo("Browser", "Chrome");
            _extentReports.AddSystemInfo("OS", "Windows");
        }

        public static void ExtentReportTearDown()
        {
            _extentReports.Flush();
            if (Directory.Exists(lastRunPath))
            {
                Directory.Delete(lastRunPath, true);
            }
            Directory.CreateDirectory(lastRunPath);
            Directory.GetFiles(testResultPath).ToList().ForEach(file =>
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(lastRunPath, fileName);
                File.Copy(file, destFile, true);
            });
        }

        public string addScreenshot(IWebDriver driver, ScenarioContext scenarioContext)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string screenshotLoc = Path.Combine(testResultPath, scenarioContext.ScenarioInfo.Title + ".png");
            screenshot.SaveAsFile(screenshotLoc);
            return screenshotLoc;
        }

    }
}
