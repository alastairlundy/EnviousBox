/*
    EnviousBox - Round
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

using System.Reflection;

using AlastairLundy.Extensions.System;

using Round.Cli.Commands;
using Round.Cli.Localizations;

using Spectre.Console.Cli;

CommandApp app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<RoundDecimalCommand>("decimal")
        .WithAlias("")
        .WithAlias("decimal-places")
        .WithAlias("dp")
        .WithDescription(Resources.Command_Round_Decimal_Description)
        .WithExample("1.6532", "2")
        .WithExample("3.1495265", "4");

    config.AddCommand<RoundSignificantFiguresCommand>("sf")
        .WithAlias("significant-figures")
        .WithAlias("significantfigures")
        .WithDescription(Resources.Command_Round_SignificantFigure_Description)
        .WithExample("0.19458", "2")
        .WithExample("5.44832", "4");

    config.SetApplicationVersion(Assembly.GetExecutingAssembly().GetName().Version.ToFriendlyVersionString());
});

app.SetDefaultCommand<RoundDecimalCommand>();

return app.Run(args);