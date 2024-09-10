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

using System.Collections.Generic;

using System.Security.Cryptography;

using Spectre.Console;

namespace Caesar;

public static class ConsoleHelper
{
    public static void PrintResults(string[] results)
    {
        var rows = new List<Text>();

        foreach (string result in results)
        {
            rows.Add(new Text(result));
        }
        
        AnsiConsole.Write(new Rows(rows));
    }

    public static int ShiftHandler(int? shiftAmount)
    {
        int shift;
        if (shiftAmount == null)
        {
            shift = RandomNumberGenerator.GetInt32(1, 26);
        }
        else
        {
            shift = (int)shiftAmount;
        }

        return shift;
    }
}