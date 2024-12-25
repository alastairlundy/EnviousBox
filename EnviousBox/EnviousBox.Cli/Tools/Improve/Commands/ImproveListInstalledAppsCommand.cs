using EnviousBox.Cli.Tools.Improve.Settings;
using Spectre.Console.Cli;

namespace EnviousBox.Cli.Tools.Improve.Commands;

public class ImproveListInstalledAppsCommand : Command<ImproveListInstalledAppsCommand.Settings>
{
    public class Settings : ImproveListSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        throw new System.NotImplementedException();
    }
}