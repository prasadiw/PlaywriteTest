using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModelPW.pages
{
    internal class NewCarsPage : BasePage
    {
        public NewCarsPage(IPage page) : base(page)
        {
        }

        public async Task<ToyotaCarsPage> GoToToyota()
        {
            await keyword.Click("NewCarsPage", "toyotacar");
            //await page.Locator("//div[normalize-space()='Toyota']").ClickAsync();

            return new ToyotaCarsPage(page);
        
        }

        public async Task<BMWCarsPage> GoToBMW()
        {
            await keyword.Click("NewCarsPage", "bmwcar");
            // await page.Locator("//div[normalize-space()='BMW']").ClickAsync();

            return new BMWCarsPage(page);
        }


        public async Task<HondaCarsPage> GoToHonda()
        {
            await keyword.Click("NewCarsPage", "hondacar");
           // await page.Locator("//div[normalize-space()='Honda']").ClickAsync();
            return new HondaCarsPage(page);
            
        }


        public async Task<MGCarsPage> GoToMG()
        {
            await keyword.Click("NewCarsPage", "mgcar");
            // await page.Locator("//div[normalize-space()='MG']").ClickAsync();
            return new MGCarsPage(page);    
        }








    }
}
