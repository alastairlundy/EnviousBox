using System.ComponentModel;
using Spectre.Console.Cli;

namespace EnviousBox.Cli.Tools.Improve.Settings;

public class ImproveListSettings : CommandSettings
{
    [CommandOption("--use <PackageManager>")]
    [DefaultValue("all")]
    public string? PackageManagerToCheck { get; init; }

}