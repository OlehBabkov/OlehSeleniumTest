using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace OlehSeleniumTest.Commands
{
    public class ClickDeleteButtonCommand(ILogger<ClickDeleteButtonCommand> logger) : ICommand
    {
        public string Name => "clickDelete";

        private readonly ILogger<ClickDeleteButtonCommand> _logger = logger;

        public Task ExecuteAsync(IWebDriver driver, Dictionary<string, string> parameters)
        {
            if (!parameters.TryGetValue("Email", out var email))
                throw new ArgumentException($"Missing Email parameter for {Name} command");

            _logger.LogInformation($"Clicking delete button for the user with Email: {email}");

            var rows = driver.FindElements(By.CssSelector(".rt-tbody .rt-tr-group"));

            foreach (var row in rows)
            {
                if (row.Text.Contains(email, StringComparison.OrdinalIgnoreCase))
                {
                    var deleteButton = row.FindElement(By.CssSelector("span[title='Delete']"));
                    deleteButton.Click();

                    _logger.LogInformation("Delete button clicked.");

                    return Task.CompletedTask;
                }
            }

            _logger.LogError("Delete button not found");
            throw new Exception("Delete button for user not found!");
        }
    }
}