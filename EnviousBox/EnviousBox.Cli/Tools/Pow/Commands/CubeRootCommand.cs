/*
    EnviousBox - Pow
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
using System.Collections.Generic;
using System.IO;

using AlastairLundy.Extensions.Collections.Strings;

using AlastairLundy.Extensions.Maths.Powers;
using EnviousBox.Cli.Localizations;
using EnviousBox.Cli.Tools.Pow.Settings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EnviousBox.Cli.Tools.Pow.Commands;

public class CubeRootCommand : Command<CubeRootCommand.Settings>
{
    public class Settings : SharedSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.Inputs == null || settings.Inputs.Length == 0)
        {
            AnsiConsole.WriteException(new NullReferenceException());
            return -1;
        }

        List<decimal> results = new List<decimal>();

        foreach (decimal input in settings.Inputs)
        {
            decimal cubeRoot = input.CubeRoot();
            
            results.Add(cubeRoot);
        }

        if (settings.OutputFile != null)
        {
            try
            {
                if (!File.Exists(settings.OutputFile))
                {
                    File.WriteAllLines(settings.OutputFile, results.ToArray().ToStringEnumerable());
                    AnsiConsole.WriteLine($"{Resources.File_Save_Success} {settings.OutputFile}");
                    return 0;
                }

                throw new ArgumentException(Resources.File_Saved_AlreadyExists);
            }
            catch(Exception exception)
            {
                AnsiConsole.WriteException(exception);
                return -1;
            }
        }
        // ReSharper disable once RedundantIfElseBlock
        else
        {
            foreach (string result in results.ToStringEnumerable())
            {
                AnsiConsole.WriteLine(result);
            }

            return 0;
        }
    }
}
