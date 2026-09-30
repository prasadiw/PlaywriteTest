using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using PageObjectModelPW.testcases;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModelPW.utilities
{
    internal class KeywordDriven
    {

        
        private IPage page;

        public KeywordDriven(IPage page) { this.page = page; }

        public async Task Click(string pageName, string locatorName)
        {

            BaseTest.test.Info("Clicking on an Element : " + locatorName);
            await page.Locator(XMLLocatorReader.GetLocatorValue(pageName, locatorName)).ClickAsync();
          
        }


        public async Task MouseOver(string pageName, string locatorName)
        {

            BaseTest.test.Info("Moving to an Element : " + locatorName);
            await page.HoverAsync(XMLLocatorReader.GetLocatorValue(pageName, locatorName));

        }



        public async Task<string> GetText(string pageName, string locatorName)
        {

            BaseTest.test.Info("Getting text of an Element : " + locatorName);
            return await page.Locator(XMLLocatorReader.GetLocatorValue(pageName, locatorName)).InnerTextAsync();
            
        }

        public async Task Type(IPage page, string pageName, string locatorName, string value)
        {

            BaseTest.test.Info("Typing in an Element : " + locatorName+" entered the value as : "+value);
            await page.Locator(XMLLocatorReader.GetLocatorValue(pageName, locatorName)).FillAsync(value);

        }


        
    }
}
