using Microsoft.Playwright;
using Org.BouncyCastle.Asn1;
using PageObjectModelPW.pages;
using PageObjectModelPW.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModelPW.testcases
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    internal class FindNewCarsTest : BaseTest
    {

        [Parallelizable(ParallelScope.Self)]
        [Test, TestCaseSource(nameof(GetTestData)),Category("BVT")]
        public async Task FindCarTest(string carbrand, string browserType,string runmode, string carTitle)
        {
            
            if (runmode.Equals("N"))
            {

                Assert.Ignore("Ignoring the test as the run mode is NO");

            }

            //Each test gets a new Playwright Instance
            using var playwrightInstance = await Playwright.CreateAsync();

          var (browser,page) = await CreateBrowserAndPage(playwrightInstance, browserType, new BrowserTypeLaunchOptions { Headless = false });


            HomePage home = new HomePage(page);
            NewCarsPage newCar = await home.FindNewCars();
            // NewCarsPage newCar = new NewCarsPage(page);



            var carBrandActions = new Dictionary<string, Func<Task>>
            {

                {"bmw", newCar.GoToBMW },
                {"honda", newCar.GoToHonda},
                {"toyota", newCar.GoToToyota },
                {"mg", newCar.GoToMG }

            };

            try
            {
                /*
                if (carbrand.Equals("bmw"))
                {
                    await newCar.GoToBMW();
                }else if (carbrand.Equals("toyota"))
                {
                    await newCar.GoToToyota();
                }else if (carbrand.Equals("mg"))
                {
                    await newCar.GoToMG();
                }*/




                if(carBrandActions.TryGetValue(carbrand.ToLower(), out var navigateToCar))
                {


                    await navigateToCar();
                    Console.WriteLine("Car title is : "+await BasePage.carBase.GetCarTitle());
                    Assert.That(carTitle.Equals(await BasePage.carBase.GetCarTitle()),"Car Titles not matching for : "+carTitle);
                
                
                }
                else
                {

                    Assert.Fail($"Car brand '{carbrand}' does not exists");
                }

                await Task.Delay(2000);
            }
            catch(Exception ex)
            {
                //capturing screenshot
                await CaptureScreenshot(page);

            }finally
            {

                await page.CloseAsync();
                await browser.CloseAsync();
            }
           

        }

        public static IEnumerable<TestCaseData> GetTestData()
        {

            var columns = new List<string> { "carbrand", "browserType", "runmode", "carTitle" };

            return DataUtil.GetTestDataFromExcel(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\resources\\testdata.xlsx", "FindCarTest", columns);

        }

    }
}
