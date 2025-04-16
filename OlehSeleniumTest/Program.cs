using System.Text.Json;
using OlehSeleniumTest.Commands;
using OlehSeleniumTest.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class Program
{
    static async Task Main(string[] args)
    {
        // Setup Chrome options.
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");

        // Start Chrome Browser.
        using var driver = new ChromeDriver(options);
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

        // Navigate to the Web Tables page
        driver.Navigate().GoToUrl("https://demoqa.com/webtables");
        TakeScreenshot(driver, "step_1_open_page");

        // Click the Add button
        driver.FindElement(By.Id("addNewRecordButton")).Click();
        wait.Until(d => d.FindElement(By.Id("firstName")).Displayed);

        // to be shure that modal window is properly displayed
        // var modal = driver.FindElement(By.ClassName("modal-content"));
        // return modal.Displayed && modal.Size.Height > 0 && modal.Size.Width > 0;
        // didn't help a lot but much better than to use nothing.
        // TODO: Probably there are other ways how to improve it...
        Thread.Sleep(300);
        TakeScreenshot(driver, "step_2_add_clicked");

        // Fill the data from JSON file.
        var jsonText = await File.ReadAllTextAsync("TestData/userData.json");
        var user = JsonSerializer.Deserialize<User>(jsonText);
        driver.FindElement(By.Id("firstName")).SendKeys(user.FirstName);
        driver.FindElement(By.Id("lastName")).SendKeys(user.LastName);
        driver.FindElement(By.Id("userEmail")).SendKeys(user.Email);
        driver.FindElement(By.Id("age")).SendKeys(user.Age);
        driver.FindElement(By.Id("salary")).SendKeys(user.Salary);
        driver.FindElement(By.Id("department")).SendKeys(user.Department);
        TakeScreenshot(driver, "step_3_fill_data");

        // Submit
        driver.FindElement(By.Id("submit")).Click();
        TakeScreenshot(driver, "step_4_submit");

        // Wait for the data in the table.
        CustomCommands.WaitForTableRowData(driver, user, 10);
        // Need to be hided, because ads are displayed on the half of the screen!
        HideAds(driver);
        TakeScreenshot(driver, "step_5_row_verified");

        // Edit the user's salary.
        int newSalary = new Random().Next(50_000, 150_000);
        user.Salary = newSalary.ToString();
        ClickEditButton(driver, user.Email);
        EditUserSalary(driver, newSalary);
        CustomCommands.WaitForTableRowData(driver, user);
        TakeScreenshot(driver, "step_6_updated_salary");

        // Delete user
        ClickDeleteButton(driver, user.Email);
        CustomCommands.WaitForTableRowDeletion(driver, user.Email);
        TakeScreenshot(driver, "step_7_delete_user");
    }

    private static void TakeScreenshot(IWebDriver driver, string stepName)
    {
        string screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
        Directory.CreateDirectory(screenshotsDir);

        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        screenshot.SaveAsFile($"Screenshots/{stepName}.png");
    }

    private static void HideAds(IWebDriver driver)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

        // Hide all ads!
        js.ExecuteScript(@"
            const elements = document.querySelectorAll('[id*=""google_ads""] , .ad, .ads, iframe, [style*=""z-index""]');
            elements.forEach(el => el.style.display = 'none');
        ");
    }

    private static void ClickEditButton(IWebDriver driver, string email)
    {
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

        throw new Exception("Edit button for user not found!");
    }

    private static void EditUserSalary(IWebDriver driver, int newSalary)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        wait.Until(d => d.FindElement(By.Id("salary")).Displayed);

        var salaryInput = driver.FindElement(By.Id("salary"));
        salaryInput.Clear();
        salaryInput.SendKeys(newSalary.ToString());

        driver.FindElement(By.Id("submit")).Click();
    }

    private static void ClickDeleteButton(IWebDriver driver, string email)
    {
        var rows = driver.FindElements(By.CssSelector(".rt-tbody .rt-tr-group"));

        foreach (var row in rows)
        {
            if (row.Text.Contains(email, StringComparison.OrdinalIgnoreCase))
            {
                var rowText = row.Text;
                Console.WriteLine($"User {rowText}");
                Console.WriteLine("would be deleted!");

                var deleteButton = row.FindElement(By.CssSelector("span[title='Delete']"));
                deleteButton.Click();

                return;
            }
        }

        throw new Exception("Delete button for user not found!");
    }
}