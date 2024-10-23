/*
    EnviousBox
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

using EnviousBox.Cli.Localizations;

using EnviousBox.Cli.Tools.Average.Commands;
using EnviousBox.Cli.Tools.Round.Commands;

using Spectre.Console.Cli;

CommandApp app = new();

app.Configure(config =>
{

    config.AddBranch("round", conf =>
    {

        conf.AddCommand<RoundDecimalCommand>("decimal")
            .WithAlias("")
            .WithAlias("decimal-places")
            .WithAlias("dp")
            .WithDescription(Resources.Apps_Round_Command_Round_Decimal_Description)
            .WithExample("1.6532", "2")
            .WithExample("3.1495265", "4");

        conf.AddCommand<RoundSignificantFiguresCommand>("sf")
            .WithAlias("significant-figures")
            .WithAlias("significantfigures")
            .WithDescription(Resources.Apps_Round_Command_Round_SignificantFigure_Description)
            .WithExample("0.19458", "2")
            .WithExample("5.44832", "4");
        
    });

    config.AddBranch("average", conf =>
    {

        conf.AddCommand<MedianCommand>("median")
            .WithDescription(Resources.Apps_Average_Command_Median_Description)
            .WithExample("1", "2", "3", "4", "5");

        conf.AddCommand<ModeCommand>("mode")
            .WithDescription(Resources.Apps_Average_Command_Mode_Description)
            .WithExample("1", "3", "3", "5", "7");

        conf.AddCommand<ArithmeticMeanCommand>("arithmetic-mean")
            .WithAlias("mean")
            .WithAlias("arithmetic mean")
            .WithAlias("arithmetic")
            .WithDescription(Resources.Apps_Average_Command_ArithmeticMean_Description)
            .WithExample("1", "4", "6", "8", "9", "13");

        conf.AddCommand<GeometricMeanCommand>("geometric-mean")
            .WithAlias("geometric mean")
            .WithAlias("geometric")
            .WithAlias("geomean")
            .WithDescription(Resources.Apps_Average_Command_GeometricMean_Description)
            .WithExample("1", "4", "6", "8", "9", "13");

        conf.AddCommand<MidRangeCommand>("midrange")
            .WithDescription(Resources.Apps_Average_Command_MidRange_Description)
            .WithExample("1", "3", "6", "9");

        conf.AddCommand<InterquartileMeanCommand>("interquartertile-mean")
            .WithAlias("iqr-mean")
            .WithDescription(Resources.Apps_Average_Command_InterquartileMean_Description)
            .WithExample("1", "2", "3", "4", "5", "6");
    });

    config.UseAssemblyInformationalVersion();
});


return app.Run(args);