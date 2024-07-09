/*
    Copyright 2024 Alastair Lundy

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AlastairLundy.Extensions.Maths.Averages;

using Average.Helpers;
using Average.Library;

using Average.Localizations;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Average.Commands.CalculationCommands;

public class ModeCommand : Command<ModeCommand.Settings>
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

        IEnumerable<decimal> modes = settings.Inputs.Mode();

        if (settings.FileOutput != null)
        {
            try
            {
                File.WriteAllLines(settings.FileOutput!, DecimalHelper.ConvertDecimalsToStrings(modes.ToArray()));
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
            AnsiConsole.Write(GridCreator.CreateGridWithRows(modes.ToArray()));
            return 0;
        }
    }
}