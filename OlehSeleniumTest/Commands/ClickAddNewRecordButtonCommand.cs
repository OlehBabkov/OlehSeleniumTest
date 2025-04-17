using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace OlehSeleniumTest.Commands
{
    public class ClickAddNewRecordButtonCommand(ILogger<ClickAddNewRecordButtonCommand> logger) : ICommand
    {
        public string Name => throw new NotImplementedException();

        private readonly ILogger<ClickAddNewRecordButtonCommand> _logger = logger;

        public Task ExecuteAsync(IWebDriver driver, Dictionary<string, string> parameters)
        {
            var addNewRecordButton = driver.FindElement(By.Id("addNewRecordButton"));
            addNewRecordButton.Click();

            _logger.LogInformation("Clicking 'Add New Record' button");

            return Task.CompletedTask;
        }
    }
}