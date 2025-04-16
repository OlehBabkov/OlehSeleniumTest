using Microsoft.Extensions.Options;
using OlehSeleniumTest.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OlehSeleniumTest.Services
{
    public class BrowserService : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public BrowserService(IOptions<CustomSettings> options)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");

            Driver = new ChromeDriver(chromeOptions);
            Driver.Navigate().GoToUrl(options.Value.BaseUrl);
        }

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}