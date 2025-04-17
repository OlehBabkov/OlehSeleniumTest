using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace OlehSeleniumTest.Commands
{
    public class RemoveAdsCommand(ILogger<RemoveAdsCommand> logger) : ICommand
    {
        public string Name => "removeAds";

        private readonly ILogger<RemoveAdsCommand> _logger = logger;

        public Task ExecuteAsync(IWebDriver driver, Dictionary<string, string> parameters)
        {
            _logger.LogInformation("Removing ads from the page...");

            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(@"
                const elements = document.querySelectorAll('[id*=""google_ads""] , .ad, .ads, iframe, [style*=""z-index""]');
                elements.forEach(el => el.style.display = 'none');
            ");

            return Task.CompletedTask;
        }
    }
}