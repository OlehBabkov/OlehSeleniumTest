using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace OlehSeleniumTest.Commands
{
    public class JsCommands(ILogger<JsCommands> logger)
    {
        private readonly ILogger<JsCommands> _logger = logger;

        public void HideAds(IWebDriver driver)
        {
            _logger.LogInformation("Removing advertisments");
            
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            js.ExecuteScript(@"
                const elements = document.querySelectorAll('[id*=""google_ads""] , .ad, .ads, iframe, [style*=""z-index""]');
                elements.forEach(el => el.style.display = 'none');");
        }
    }
}