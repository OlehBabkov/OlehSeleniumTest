using Microsoft.Extensions.Logging;
using OlehSeleniumTest.Models;
using OpenQA.Selenium;

namespace OlehSeleniumTest.Commands
{
    public class FillUserFormCommand(ILogger<FillUserFormCommand> logger) : ICommand
    {
        public string Name => "fillUserForm";

        private readonly ILogger<FillUserFormCommand> _logger = logger;

        public Task ExecuteAsync(IWebDriver driver, Dictionary<string, string> parameters)
        {
            var userData = new User
            {
                FirstName = parameters["FirstName"],
                LastName = parameters["LastName"],
                Email = parameters["Email"],
                Age = parameters["Age"],
                Salary = parameters["Salary"],
                Department = parameters["Department"]
            };

            _logger.LogInformation($"Filling form for user {userData.FirstName} {userData.LastName}");

            // Find the input elements and fill them
            driver.FindElement(By.Id("firstName")).SendKeys(userData.FirstName);
            driver.FindElement(By.Id("lastName")).SendKeys(userData.LastName);
            driver.FindElement(By.Id("userEmail")).SendKeys(userData.Email);
            driver.FindElement(By.Id("age")).SendKeys(userData.Age);
            driver.FindElement(By.Id("salary")).SendKeys(userData.Salary);
            driver.FindElement(By.Id("department")).SendKeys(userData.Department);

            _logger.LogInformation("Form is filled");

            return Task.CompletedTask;
        }
    }
}