using Microsoft.Playwright;
using PageObjectModelPW.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModelPW.pages
{
    internal class CarBase
    {

        IPage page;


        //car names - //div/div/div/div/a/h3
        //car prices - //div/div/div[3]/div/span/span[1]

        public CarBase(IPage page)
        {
            this.page = page;
        }

        public async Task<string> GetCarTitle()
        {
            return await BasePage.keyword.GetText("CarBase", "cartitle");
           // return await page.Locator("//header/h1").InnerTextAsync();
        }



        public async Task GetCarNameAndPrices()
        {
            await Task.Delay(3000);

            for(int i = 0;i< await page.Locator("//div/div/div[3]/div/span/span[1]").CountAsync(); i++)
            {

                Console.WriteLine(await page.Locator("//div/div/div/div/a/h3").Nth(i).InnerTextAsync() + "-----price is : " + await page.Locator("//div/div/div[3]/div/span/span[1]").Nth(i).InnerTextAsync());

            }
        }



    }
}
