using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using EmpApp.Utility;
using EmpApp.Variable;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using Reqnroll.BoDi;

namespace EmpApp.Hooks
{
    [Binding]
    public sealed class Hooks : ExtentReport
    {
        private readonly IObjectContainer _container;
        public static ConfigSetting config;
        static string configPath = dir.Replace("bin\\Debug\\net10.0", "Configuration\\configsetting.json");

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("Running before test run...");
            config = new ConfigSetting();

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(configPath);
            IConfigurationRoot configuration = builder.Build();
            configuration.Bind(config);
            Console.WriteLine($"Browser type from config: {config.BrowserType}");

            ExtentReportInit();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Running after test run...");
            ExtentReportTearDown();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Console.WriteLine("Running before feature...");
            _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Console.WriteLine("Running after feature...");
        }


        [BeforeScenario("@tag1")]
        public void BeforeScenarioWithTag()
        {
            Console.Write("Running inside tagged hooks in Reqnroll");
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            IWebDriver driver;
            //Launch the browser
            if (Hooks.config.BrowserType.ToLower() == "chrome")
            {
                driver = new ChromeDriver();
            }
            else if (Hooks.config.BrowserType.ToLower() == "edge")
            {
                driver = new EdgeDriver();
            }
            else if (Hooks.config.BrowserType.ToLower() == "firefox")
            {
                driver = new FirefoxDriver();
            }
            else
            {
                throw new Exception("Browser type not supported");
            }

            driver.Manage().Window.Maximize();

            //Store the driver instance in the container for later use
            _container.RegisterInstanceAs<IWebDriver>(driver);

            //Creating a node under a feature
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //Retrieve the driver instance from the container
            var driver = _container.Resolve<IWebDriver>();

            //If browser is open, close it
            if (driver != null)
            {
                driver.Quit();
            }
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            var driver = _container.Resolve<IWebDriver>();
            Console.WriteLine("Running after step...");
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;


            //When scenario is passed
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName);
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName);
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName);
                }
                else if (stepType == "And")
                {
                    _scenario.CreateNode<And>(stepName);
                }
            }
            //When scenario is failed
            else
            {

                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                         MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                else if (stepType == "And")
                {
                    _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
            }

        }
    }
}