﻿/*
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

using System;
using System.Globalization;
using System.IO;
using AlastairLundy.Extensions.Maths.Averages;
using EnviousBox.Cli.Localizations;
using EnviousBox.Cli.Tools.Average.Helpers;
using EnviousBox.Cli.Tools.Average.Settings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EnviousBox.Cli.Tools.Average.Commands;

public class MedianCommand : Command<MedianCommand.Settings>
{
    public class Settings : BaseSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.Inputs == null)
        {
            AnsiConsole.WriteException(new NullReferenceException());
            return -1;
        }

        decimal median = settings.Inputs.Median();

        if (settings.FileOutput != null)
        {
            try
            {
                File.WriteAllLines(settings.FileOutput!, new []{median.ToString(CultureInfo.CurrentCulture)});
                AnsiConsole.WriteLine($"{Resources.File_Save_Success} {settings.FileOutput}");

                return 0;
            }
            catch(Exception exception)
            {
                AnsiConsole.WriteLine($"{Resources.File_Save_Error}");
                AnsiConsole.WriteException(exception);
                return -1;
            }
        }
        // ReSharper disable once RedundantIfElseBlock
        else
        {
            AnsiConsole.Write(GridCreator.CreateGridWithRows(new []{median}));
            return 0;
        }
    }
}