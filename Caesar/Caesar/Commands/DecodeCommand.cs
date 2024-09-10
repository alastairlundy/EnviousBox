/*
    EnviousBox - Caesar
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
using System.ComponentModel;
using System.IO;

using Caesar.Library;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Caesar.Commands;

public class DecodeCommand : Command<DecodeCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Words>")]
        public string[]? Words { get; init; }
        
        [CommandOption("-o|--output:<file>")]
        public string? OutputFile { get; init; }
            
        [CommandOption("-s|--shift:<number_to_shift_by>")]
        [DefaultValue(null)]
        public int? ShiftAmount { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        int shift = ConsoleHelper.ShiftHandler(settings.ShiftAmount);

        if (settings.Words == null)
        {
            AnsiConsole.WriteException(new NullReferenceException());
            return -1;
        }
        
        CaesarCipher caesarCipher = new CaesarCipher();
        
        string[] newValues = caesarCipher.Decode(settings.Words!, shift);

        if (settings.OutputFile != null)
        {
            try
            {
                File.WriteAllLines(settings.OutputFile, newValues);
                return 0;
            }
            catch (Exception exception)
            {
                AnsiConsole.WriteException(exception);
                return -1;
            }
        }
        
        ConsoleHelper.PrintResults(newValues);
        return 0;
    }
}