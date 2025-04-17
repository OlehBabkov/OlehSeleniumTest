using Microsoft.Extensions.DependencyInjection;

namespace OlehSeleniumTest.Commands
{
    public class CommandFactory
    {
        private readonly Dictionary<string, Func<IServiceProvider, ICommand>> _commandFactories;

        public CommandFactory()
        {
            _commandFactories = new Dictionary<string, Func<IServiceProvider, ICommand>>(StringComparer.OrdinalIgnoreCase)
            {
                { "openUrl", sp => sp.GetRequiredService<OpenUrlCommand>() },
                { "removeAds", sp => sp.GetRequiredService<RemoveAdsCommand>() },
                { "clickAddNewRecordButton", sp => sp.GetRequiredService<ClickAddNewRecordButtonCommand>() },
                { "fillUserForm", sp => sp.GetRequiredService<FillUserFormCommand>() },
                { "submitForm", sp => sp.GetRequiredService<SubmitFormCommand>() },
                { "waitForTableRowData", sp => sp.GetRequiredService<WaitForTableRowDataCommand>() },
                { "clickEdit", sp => sp.GetRequiredService<ClickEditButtonCommand>() },
                { "editUserSalary", sp => sp.GetRequiredService<EditUserSalaryCommand>() },
                { "clickDelete", sp => sp.GetRequiredService<ClickDeleteButtonCommand>() },
                { "waitForTableRowDeletion", sp => sp.GetRequiredService<WaitForTableRowDeletionCommand>() }
            };
        }

        public ICommand CreateCommand(string type, IServiceProvider services)
        {
            if (!_commandFactories.TryGetValue(type, out var factory))
                throw new InvalidOperationException($"Command not registered: {type}");

            return factory(services);
        }
    }
}