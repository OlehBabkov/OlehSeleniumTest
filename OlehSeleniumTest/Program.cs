using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OlehSeleniumTest.Commands;
using OlehSeleniumTest.Configuration;
using OlehSeleniumTest.Models;
using OlehSeleniumTest.Services;
using OpenQA.Selenium;

public class Program
{
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<CustomSettings>(context.Configuration.GetSection("CustomSettings"));

                services.AddSingleton<CustomCommands>();
                services.AddSingleton<JsCommands>();
                services.AddSingleton<TableActions>();
                services.AddSingleton<BrowserService>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
            })
            .Build();

        var browser = host.Services.GetRequiredService<BrowserService>();
        var custom = host.Services.GetRequiredService<CustomCommands>();
        var js = host.Services.GetRequiredService<JsCommands>();
        var actions = host.Services.GetRequiredService<TableActions>();
        var driver = browser.Driver;

        js.HideAds(driver);

        custom.TakeScreenshot(driver, "step_1_open_page");
        driver.FindElement(By.Id("addNewRecordButton")).Click();
        Thread.Sleep(300);
        custom.TakeScreenshot(driver, "step_2_add_clicked");

        var json = await File.ReadAllTextAsync("TestData/userData.json");
        var user = JsonSerializer.Deserialize<User>(json);

        actions.FillUserForm(driver, user!);
        custom.TakeScreenshot(driver, "step_3_fill_data");
        driver.FindElement(By.Id("submit")).Click();
        custom.TakeScreenshot(driver, "step_4_submit");

        custom.WaitForTableRowData(driver, user!);
        custom.TakeScreenshot(driver, "step_5_row_verified");

        int newSalary = new Random().Next(50_000, 150_000);
        user!.Salary = newSalary.ToString();
        actions.ClickEditButton(driver, user.Email);
        actions.EditUserSalary(driver, newSalary);
        custom.WaitForTableRowData(driver, user);
        custom.TakeScreenshot(driver, "step_6_updated_salary");

        actions.ClickDeleteButton(driver, user.Email);
        custom.WaitForTableRowDeletion(driver, user.Email);
        custom.TakeScreenshot(driver, "step_7_delete_user");

        browser.Dispose();
    }
}