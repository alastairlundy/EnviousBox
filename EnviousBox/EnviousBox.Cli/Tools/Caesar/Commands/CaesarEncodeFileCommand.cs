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
using System.Linq;
using AlastairLundy.CaesarLib;
using EnviousBox.Cli.Tools.Caesar.Helpers;

using Spectre.Console;
using Spectre.Console.Cli;

namespace EnviousBox.Cli.Tools.Caesar.Commands;

public class CaesarEncodeFileCommand : Command<CaesarEncodeFileCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Files>")]
        public string[]? Files { get; init; }
        
        [CommandOption("-o|--output:<file>")]
        public string? OutputFiles { get; init; }
            
        [CommandOption("-s|--shift:<number_to_shift_by>")]
        public int? ShiftAmount { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.Files == null)
        {
            AnsiConsole.WriteException(new FileNotFoundException());
            return -1;
        }
        
        int shift = ConsoleHelper.ShiftHandler(settings.ShiftAmount);

        CaesarCipher caesarCipher = new CaesarCipher();

        List<string> newValues = new List<string>();
        
        foreach (string file in settings.Files)
        {
            string[] newWords = caesarCipher.Encode(File.ReadAllLines(file), shift).ToArray();

            foreach (string word in newWords)
            {
                newValues.Add(word);
            }
        }

        if (settings.OutputFiles is null)
        {
            try
            {
                File.WriteAllLines(settings.Files.First(), newValues);
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
                File.WriteAllLines(settings.OutputFiles, newValues);
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