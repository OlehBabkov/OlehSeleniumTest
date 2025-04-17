using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OlehSeleniumTest.Configuration;
using OlehSeleniumTest.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OlehSeleniumTest.Commands
{
    public class TableActions
    {
        private readonly ILogger<TableActions> _logger;

        public TableActions(ILogger<TableActions> logger)
        {
            _logger = logger;
        }

        public void FillUserForm(IWebDriver driver, User user)
        {
            _logger.LogInformation("Filling a form.");

            driver.FindElement(By.Id("firstName")).SendKeys(user.FirstName);
            driver.FindElement(By.Id("lastName")).SendKeys(user.LastName);
            driver.FindElement(By.Id("userEmail")).SendKeys(user.Email);
            driver.FindElement(By.Id("age")).SendKeys(user.Age);
            driver.FindElement(By.Id("salary")).SendKeys(user.Salary);
            driver.FindElement(By.Id("department")).SendKeys(user.Department);
        }

        public void ClickEditButton(IWebDriver driver, string email)
        {
            _logger.LogInformation("Clicking an edit button");

            var rows = driver.FindElements(By.CssSelector(".rt-tbody .rt-tr-group"));

            foreach (var row in rows)
            {
                if (row.Text.Contains(email, StringComparison.OrdinalIgnoreCase))
                {
                    var editButton = row.FindElement(By.CssSelector("span[title='Edit']"));
                    editButton.Click();

                    return;
                }
            }

            _logger.LogError("Edit button not found");
            throw new Exception("Edit button for user not found!");
        }

        public void ClickDeleteButton(IWebDriver driver, string email)
        {
            _logger.LogInformation("Clicking delete button");

            var rows = driver.FindElements(By.CssSelector(".rt-tbody .rt-tr-group"));

            foreach (var row in rows)
            {
                if (row.Text.Contains(email, StringComparison.OrdinalIgnoreCase))
                {
                    var deleteButton = row.FindElement(By.CssSelector("span[title='Delete']"));
                    deleteButton.Click();

                    return;
                }
            }

            _logger.LogError("Delete button not found");
            throw new Exception("Delete button for user not found!");
        }

        public void EditUserSalary(IWebDriver driver, int newSalary)
        {
            _logger.LogInformation("Editing a salary.");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(By.Id("salary")).Displayed);

            var salaryInput = driver.FindElement(By.Id("salary"));
            salaryInput.Clear();
            salaryInput.SendKeys(newSalary.ToString());

            driver.FindElement(By.Id("submit")).Click();
        }
    }
}