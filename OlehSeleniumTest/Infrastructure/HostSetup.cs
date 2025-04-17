using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OlehSeleniumTest.Commands;

namespace OlehSeleniumTest.Infrastructure
{
    public static class HostSetup
    {
        public static IHost Build()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<CommandFactory>();
                    services.AddSingleton<OpenUrlCommand>();
                    services.AddSingleton<RemoveAdsCommand>();
                    services.AddSingleton<ClickAddNewRecordButtonCommand>();
                    services.AddSingleton<FillUserFormCommand>();
                    services.AddSingleton<SubmitFormCommand>();
                    services.AddSingleton<WaitForTableRowDataCommand>();
                    services.AddSingleton<ClickEditButtonCommand>();
                    services.AddSingleton<EditUserSalaryCommand>();
                    services.AddSingleton<ClickDeleteButtonCommand>();
                    services.AddSingleton<WaitForTableRowDeletionCommand>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .Build();
        }
    }
}