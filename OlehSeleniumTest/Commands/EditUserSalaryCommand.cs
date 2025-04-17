using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OlehSeleniumTest.Commands
{
    public class EditUserSalaryCommand(ILogger<EditUserSalaryCommand> logger) : ICommand
    {
        public string Name => "editUserSalary";

        private readonly ILogger<EditUserSalaryCommand> _logger = logger;

        public Task ExecuteAsync(IWebDriver driver, Dictionary<string, string> parameters)
        {
            if (!parameters.TryGetValue("Salary", out var salary))
                throw new ArgumentException($"Missing Salary parameter for {Name} command");

            _logger.LogInformation($"Editing User's salary to a new ({salary})");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(By.Id("salary")).Displayed);

            var salaryInput = driver.FindElement(By.Id("salary"));
            salaryInput.Clear();
            salaryInput.SendKeys(salary);

            _logger.LogInformation("Salary updated");

            // Submit the form
            var submitButton = driver.FindElement(By.Id("submit"));
            submitButton.Click();

            _logger.LogInformation("Form submitted with a new salary");

            return Task.CompletedTask;
        }
    }
}