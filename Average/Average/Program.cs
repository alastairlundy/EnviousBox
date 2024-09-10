/*
    EnviousBox - Average
    Copyright (C) 2024 Alastair Lundy

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */


using Average.Commands;
using Average.Commands.CalculationCommands;
using Average.Localizations;

using Spectre.Console.Cli;

CommandApp app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<MedianCommand>("median")
        .WithDescription(Resources.Command_Median_Description)
        .WithExample("1", "2", "3", "4", "5");

    config.AddCommand<ModeCommand>("mode")
       .WithDescription(Resources.Command_Mode_Description)
       .WithExample("1", "3", "3", "5", "7");

    config.AddCommand<ArithmeticMeanCommand>("arithmetic-mean")
       .WithAlias("mean")
       .WithAlias("arithmetic mean")
       .WithAlias("arithmetic")
       .WithDescription(Resources.Command_ArithmeticMean_Description)
       .WithExample("1", "4", "6", "8", "9", "13");

    config.AddCommand<GeometricMeanCommand>("geometric-mean")
       .WithAlias("geometric mean")
       .WithAlias("geometric")
       .WithAlias("geomean")
       .WithDescription(Resources.Command_GeometricMean_Description)
       .WithExample("1", "4", "6", "8", "9", "13");

    config.AddCommand<MidRangeCommand>("midrange")
       .WithDescription(Resources.Command_MidRange_Description)
       .WithExample("1", "3", "6", "9");

    config.AddCommand<InterquartileMeanCommand>("interquartertile-mean")
       .WithAlias("iqr-mean")
       .WithDescription(Resources.Command_InterquartileMean_Description)
       .WithExample("1", "2", "3", "4", "5", "6");

    config.UseAssemblyInformationalVersion();
});

return app.Run(args);
