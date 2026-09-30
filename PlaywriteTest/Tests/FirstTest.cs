using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace PlaywriteTest.Tests
{
    internal class FirstTest
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            //1. creating aplaywright instance
            using var playwright = await Playwright.CreateAsync();

            //2. launch new browser instance - chromium, Firefox, webkit(safari)
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Channel = "chrome",
                Headless = false
            });

            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = 1920,
                    Height = 1080
                }
            });

            //3. create a new browser tab or page
            var page = await browser.NewPageAsync();

            //await page.SetViewportSizeAsync(1920, 1080);

            //4. navigate to a website
            await page.GotoAsync("https://www.google.com/");

            var title = await page.TitleAsync();
            Console.WriteLine($"Page title: {title}");

            await Task.Delay(5000);

            await page.GotoAsync("https://www.gmail.com/");
            await Task.Delay(2000);

            await page.GoBackAsync();
            await Task.Delay(2000);
            await page.GoForwardAsync();
            await Task.Delay(2000);
            await page.ReloadAsync();
            await Task.Delay(5000);
        }
    }
}
