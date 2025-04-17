using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace OlehSeleniumTest.Commands
{
    public class SubmitFormCommand(ILogger<SubmitFormCommand> logger) : ICommand
    {
        public string Name => "submitForm";

        private readonly ILogger<SubmitFormCommand> _logger = logger;

        public Task ExecuteAsync(IWebDriver driver, Dictionary<string, string> parameters)
        {
            var submitButton = driver.FindElement(By.Id("submit"));
            submitButton.Click();

            _logger.LogInformation("Submitting the form");

            return Task.CompletedTask;
        }
    }
}