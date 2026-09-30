using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModelPW.pages
{
    internal class HomePage : BasePage
    {
        public HomePage(IPage page) : base(page)
        {
        }

        public async Task<NewCarsPage> FindNewCars()
        {
            // await page.Locator("//div[normalize-space()='NEW CARS']").HoverAsync();
            //  await page.Locator("(//div[normalize-space()='Find New Cars'])[1]").ClickAsync();
            await keyword.MouseOver("HomePage", "newcars");
            await keyword.Click("HomePage", "findnewcars");
            return new NewCarsPage(page);
        }


        public async void SearchCars()
        {

            await page.Locator("//input[contains(@placeholder,'Type to select car name, e.g. Jeep Compass')]").FillAsync("BMW");


        }




        public void GoToPopularCars()
        {



        }



        public void GoToUpcomingCars()
        {



        }







    }
}
