using Spectre.Console.Cli;

namespace EnviousBox.Cli.Tools.Improve.Commands;

public class SpecificUpdate : Command<SpecificUpdate.Settings>
{
    public class Settings : CommandSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        throw new System.NotImplementedException();
    }
}