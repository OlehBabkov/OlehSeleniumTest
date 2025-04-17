using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace OlehSeleniumTest.Commands
{
    public class OpenUrlCommand(ILogger<OpenUrlCommand> logger) : ICommand
    {
        public string Name => "openUrl";

        private readonly ILogger<OpenUrlCommand> _logger = logger;

        public Task ExecuteAsync(IWebDriver driver, Dictionary<string, string> parameters)
        {
            if (!parameters.TryGetValue("url", out var url))
                throw new ArgumentException($"Missing {nameof(url)} parameter for {Name} command");

            _logger.LogInformation($"Navigating to URL: {url}");
            driver.Navigate().GoToUrl(url);

            return Task.CompletedTask;
        }
    }
}