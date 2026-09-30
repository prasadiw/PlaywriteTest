using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter.Config;
using AventStack.ExtentReports.Reporter;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework.Interfaces;

namespace PageObjectModelPW.testcases
{
    internal class BaseTest
    {

        /*
         * Playwright, ExtentReports, Logs, Configuration, fix, captureshots etc
         * 
         * 
         */

        protected IPlaywright playwright;
        private static ExtentReports extent;
        public static ExtentTest test;

        private static readonly ILog log = LogManager.GetLogger(typeof(BaseTest));
        IConfiguration configuration;
        private static string fileName;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            log.Info("Test Execution Started !!!");

            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\resources\\")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            DateTime currentTime = DateTime.Now;
            string fileName = "Extent_" + currentTime.ToString("yyyy-MM-dd_HH-mm-ss") + ".html";
            extent = CreateInstance(fileName);


        }


        public static ExtentReports CreateInstance(string fileName)
        {


            var htmlReporter = new ExtentSparkReporter(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\reports\\" + fileName);
            htmlReporter.Config.Theme = Theme.Standard;
            htmlReporter.Config.DocumentTitle = "Way2Automation Test Suite";
            htmlReporter.Config.ReportName = "Automation Test Results";
            htmlReporter.Config.Encoding = "utf-8";

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            extent.AddSystemInfo("Automation Tester", "Rahul Arora");
            extent.AddSystemInfo("Organization", "Way2Automation");
            extent.AddSystemInfo("Build No: ", "W2A-1234");

            return extent;
        }



        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();
            log.Info("Test Execution completed !!!");

        }

        [SetUp]
        public async Task BeforeEachTest()
        {
            test = extent.CreateTest($"{TestContext.CurrentContext.Test.ClassName} - {TestContext.CurrentContext.Test.Name}");

            playwright = await Playwright.CreateAsync();

        }


        public static async Task CaptureScreenshot(IPage page)
        {
            DateTime currentTime = DateTime.Now;
            fileName = currentTime.ToString("yyyy-MM-dd_HH-mm-ss") + ".jpg";

            await page.ScreenshotAsync(new PageScreenshotOptions { Path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\reports\\" + fileName });

            Console.WriteLine("File name is : " + fileName);
            test.Fail("<b><font color=red>  Screenshot of failure </font></b><br>", MediaEntityBuilder.CreateScreenCaptureFromPath(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\reports\\" + fileName).Build());

        }

        [TearDown]
        public void AfterEachTest()
        {
            //Get the test status
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            string message = TestContext.CurrentContext.Result.Message;

            switch (testStatus)
            {
                case TestStatus.Passed:
                    test.Pass("Test Passed");
                    IMarkup markup = MarkupHelper.CreateLabel("PASS", ExtentColor.Green);
                    test.Pass(markup);
                    break;

                case TestStatus.Skipped:
                    test.Skip($"Test Skipped: {message}");
                    markup = MarkupHelper.CreateLabel("SKIP", ExtentColor.Amber);
                    test.Skip(markup);
                    break;

                case TestStatus.Failed:
                    test.Fail($"Test Failed: {message}");
                    test.Fail("<b><font color=red>  Screenshot of failure </font></b><br>", MediaEntityBuilder.CreateScreenCaptureFromPath(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\reports\\" + fileName).Build());
                    markup = MarkupHelper.CreateLabel("FAIL", ExtentColor.Red);
                    test.Fail(markup);
                    break;
            }

            playwright?.Dispose();


        }

        protected async Task<(IBrowser, IPage)> CreateBrowserAndPage(IPlaywright playwrightInstance, string browserType, BrowserTypeLaunchOptions launchOptions = null)
        {
            IBrowser browser;
            if (browserType.Equals("chrome",StringComparison.OrdinalIgnoreCase)){

                browser = await playwrightInstance.Chromium.LaunchAsync(launchOptions); ;
            }
            else if (browserType.Equals("firefox", StringComparison.OrdinalIgnoreCase))
            {

                browser = await playwrightInstance.Firefox.LaunchAsync(launchOptions); ;
            }
            else
            {

                Assert.Fail("Invalid browser type : " + browserType);
                return(null, null);
            }



            IPage page = await browser.NewPageAsync();
            await page.SetViewportSizeAsync(1920, 1080);
            await page.GotoAsync(configuration["Appsettings:testsiteurl"]);

            return (browser, page);


        }



    }
}
