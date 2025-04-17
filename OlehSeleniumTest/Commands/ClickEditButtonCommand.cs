using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace OlehSeleniumTest.Commands
{
    public class ClickEditButtonCommand(ILogger<ClickEditButtonCommand> logger) : ICommand
    {
        public string Name => "clickEdit";

        private readonly ILogger<ClickEditButtonCommand> _logger = logger;

        public Task ExecuteAsync(IWebDriver driver, Dictionary<string, string> parameters)
        {
            if (!parameters.TryGetValue("Email", out var email))
                throw new ArgumentException($"Missing Email parameter for {Name} command");

            _logger.LogInformation($"Clicking edit button for user with Emial {email}");

            var rows = driver.FindElements(By.CssSelector(".rt-tbody .rt-tr-group"));

            foreach (var row in rows)
            {
                if (row.Text.Contains(email, StringComparison.OrdinalIgnoreCase))
                {
                    var editButton = row.FindElement(By.CssSelector("span[title='Edit']"));
                    editButton.Click();

                    _logger.LogInformation($"Edit button for {email} is found and clicked");

                    return Task.CompletedTask;
                }
            }

            _logger.LogError($"No such user with {email} was found");

            return Task.CompletedTask;
        }
    }
}