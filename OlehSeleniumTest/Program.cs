using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using OlehSeleniumTest.Commands;
using OlehSeleniumTest.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class Program
{
    static async Task Main(string[] args)
    {
        var host = HostSetup.Build();

        try
        {
            var factory = host.Services.GetRequiredService<CommandFactory>();

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            using var driver = new ChromeDriver(chromeOptions);

            var json = await File.ReadAllTextAsync("TestData/testScript.json");
            var commands = JsonDocument.Parse(json).RootElement.EnumerateArray();

            int step = 0;
            foreach (var commandJson in commands)
            {
                var type = commandJson.GetProperty("type").GetString()!;
                var parameters = commandJson.GetProperty("parameters")
                    .EnumerateObject()
                    .ToDictionary(p => p.Name, p => p.Value.GetString()!);

                var command = factory.CreateCommand(type, host.Services);
                await command.ExecuteAsync(driver, parameters);

                // Screenshot
                Thread.Sleep(200);
                Directory.CreateDirectory("Screenshots");
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile($"Screenshots/step_{++step}_{type}.png");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception was thrown: " + ex.Message);
        }
        finally
        {
            host.Dispose();
        }
    }
}