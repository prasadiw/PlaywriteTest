using Microsoft.Playwright;
using PageObjectModelPW.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModelPW.pages
{
    internal class BasePage
    {

        public IPage page;
        public static CarBase carBase;
        public static KeywordDriven keyword;


        public BasePage(IPage page) {
        
            this.page = page;
            carBase = new CarBase(page);
            keyword = new KeywordDriven(page);
        }
    }
}
