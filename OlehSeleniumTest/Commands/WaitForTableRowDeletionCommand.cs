using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OlehSeleniumTest.Commands
{
    public class WaitForTableRowDeletionCommand(ILogger<WaitForTableRowDeletionCommand> logger) : ICommand
    {
        public string Name => "waitForTableRowDeletion";

        private readonly ILogger<WaitForTableRowDeletionCommand> _logger = logger;

        public Task ExecuteAsync(IWebDriver driver, Dictionary<string, string> parameters)
        {
            if (!parameters.TryGetValue("Email", out var email))
                throw new ArgumentException($"Missing Email parameter for {Name} command");

            _logger.LogInformation("Waiting for a deletion");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d =>
            {
                var rows = d.FindElements(By.CssSelector(".rt-tbody .rt-tr-group"));

                return !rows.Any(row => row.Text.Contains(email, StringComparison.OrdinalIgnoreCase));
            });

            _logger.LogInformation("User was successfully deleted!");

            return Task.CompletedTask;
        }
    }
}