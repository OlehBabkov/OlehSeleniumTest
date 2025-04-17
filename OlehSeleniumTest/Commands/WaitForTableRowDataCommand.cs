using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OlehSeleniumTest.Commands
{
    public class WaitForTableRowDataCommand(ILogger<WaitForTableRowDataCommand> logger) : ICommand
    {
        public string Name => "waitForTableRowData";

        private readonly ILogger<WaitForTableRowDataCommand> _logger = logger;

        public Task ExecuteAsync(IWebDriver driver, Dictionary<string, string> parameters)
        {
            if (!parameters.TryGetValue("Email", out var email))
                throw new ArgumentException($"Missing Email parameter for {Name} command");

            _logger.LogInformation($"Waiting for the table row with email {email} to appear");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d => d.FindElements(By.CssSelector(".rt-tbody .rt-tr-group"))
                .Any(row => row.Text.Contains(email, StringComparison.OrdinalIgnoreCase)));

            _logger.LogInformation($"Table row with Email {email} was found!");

            return Task.CompletedTask;
        }
    }
}