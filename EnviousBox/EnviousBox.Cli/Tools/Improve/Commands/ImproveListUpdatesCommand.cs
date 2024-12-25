

using System.ComponentModel;
using EnviousBox.Cli.Tools.Improve.Settings;
using Spectre.Console.Cli;

namespace EnviousBox.Cli.Tools.Improve.Commands;

public class ImproveListUpdatesCommand : Command<ImproveListUpdatesCommand.Settings>
{
    public class Settings : ImproveListSettings
    {
       [CommandOption("--check <Apps>")]
        public string[]? AppsToCheck { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        bool checkAllApps = settings.AppsToCheck == null;

        if (checkAllApps)
        {
            
        }
    }
}