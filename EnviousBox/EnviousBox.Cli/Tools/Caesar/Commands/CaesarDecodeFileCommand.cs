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
using System.Collections.Generic;
using System.IO;

using EnviousBox.Cli.Tools.Caesar.Helpers;

using Spectre.Console;
using Spectre.Console.Cli;

namespace EnviousBox.Cli.Tools.Caesar.Commands;

public class CaesarDecodeFileCommand : Command<CaesarDecodeFileCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Files>")]
        public string? File { get; init; }
        
        [CommandOption("-o|--output:<file>")]
        public string? OutputFile { get; init; }
            
        [CommandOption("-s|--shift:<number_to_shift_by>")]
        public int? ShiftAmount { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.File == null)
        {
            AnsiConsole.WriteException(new FileNotFoundException());
            return -1;
        }
        
        int shift = ConsoleHelper.ShiftHandler(settings.ShiftAmount);

        CaesarCipher caesarCipher = new CaesarCipher();

        List<string> newValues = new List<string>();
        
        string[] lines = File.ReadAllLines(settings.File);

        foreach (string line in lines)
        {
            string[] newWords = caesarCipher.Decode(File.ReadAllLines(line), shift);

            foreach (string word in newWords)
            {
                newValues.Add(word);
            }
        }

        if (settings.OutputFile == null)
        {
            try
            {
                File.WriteAllLines(settings.File, newValues);
                return 0;
            }
            catch (Exception exception)
            {
                AnsiConsole.WriteException(exception);
                return -1;
            }
        }
        else
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
    }
}