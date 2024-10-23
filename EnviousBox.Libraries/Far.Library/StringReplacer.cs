/*
    EnviousBox - Far Library
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
using System.Linq;
using System.Text;

using AlastairLundy.Extensions.System;
using AlastairLundy.Extensions.System.Matching;

using Far.Library.Abstractions.Replacers;
using Far.Library.Models;

namespace Far.Library;

public class StringReplacer : IStringReplacer
{
    /// <summary>
    /// Replaces a string at a specified position.
    /// </summary>
    /// <param name="original">The string to be replaced.</param>
    /// <param name="itemPosition">The position of the string to be replaced.</param>
    /// <param name="replacement">The replacement string.</param>
    /// <returns>The modified string with the specified original string replaced.</returns>
    public string Replace(string original, StringPosition itemPosition, string replacement)
    {
        if (original.Split(Environment.NewLine).Length > 0)
        {
            string[] lines = original.Split(Environment.NewLine);

            for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
            {
                if (lineNumber == itemPosition.StartPosition.LineNumber)
                {
                    original = original.Remove(itemPosition.StartPosition.ColumnNumber, 
                        Math.Abs(itemPosition.EndPosition.ColumnNumber - itemPosition.StartPosition.ColumnNumber));
                    original = original.Insert(itemPosition.StartPosition.ColumnNumber, replacement);
                }
            }
        }
        else
        {
            original = original.Remove(itemPosition.StartPosition.ColumnNumber, 
                Math.Abs(itemPosition.EndPosition.ColumnNumber - itemPosition.StartPosition.ColumnNumber));
            original = original.Insert(itemPosition.StartPosition.ColumnNumber, replacement);
        }
        
        return original;
    }

    /// <summary>
    /// Replaces a string at a specified position.
    /// </summary>
    /// <param name="original">The string to be replaced.</param>
    /// <param name="itemToBeReplaced">The position of the string to be replaced.</param>
    /// <param name="replacement">The replacement string.</param>
    /// <returns>The modified string.</returns>
    public string Replace(string original, SearchResultItem itemToBeReplaced, string replacement)
    {
        foreach (StringPosition position in itemToBeReplaced.ResultPositions)
        {
            if (original.Split(Environment.NewLine).Length > 0)
            {
                string[] lines = original.Split(Environment.NewLine);

                foreach (string line in lines)
                {
                    if (line.Contains(itemToBeReplaced.ResultValue))
                    {
                        original = original.Remove(position.StartPosition.ColumnNumber, 
                            Math.Abs(position.EndPosition.ColumnNumber - position.StartPosition.ColumnNumber));
                        original = original.Insert(position.StartPosition.ColumnNumber, replacement);
                    }
                }
            }
            else
            {
                original = original.Remove(position.StartPosition.ColumnNumber, 
                    Math.Abs(position.EndPosition.ColumnNumber - position.StartPosition.ColumnNumber));
                original = original.Insert(position.StartPosition.ColumnNumber, replacement);
            }
        }

        return original;
    }

    /// <summary>
    /// Replaces a specified string in an IEnumerable of strings with a specified replacement character.
    /// </summary>
    /// <param name="enumerable">The enumerable to be searched and modified.</param>
    /// <param name="itemToBeReplaced">The search result item to be replaced.</param>
    /// <param name="replacement">The replacement string</param>
    /// <returns>The modified IEnumerable of strings.</returns>
    public IEnumerable<string> Replace(IEnumerable<string> enumerable, SearchResultItem itemToBeReplaced, string replacement)
    {
        string[] output = enumerable.ToArray();

        for(int index = 0; index < output.Length; index++)
        {
            output[index] = Replace(output[index], itemToBeReplaced, replacement);
        }
        
        return output;
    }

    /// <summary>
    /// Replaces partial matches of a specified string within a string with a specified replacement string.
    /// </summary>
    /// <param name="original">The string to be searched.</param>
    /// <param name="toBeReplaced">The string to be replaced.</param>
    /// <param name="replacementString">The replacement string.</param>
    /// <returns>The modified string if the string to be replaced was found; The unmodified original string otherwise.</returns>
    public string ReplacePartialMatch(string original, string toBeReplaced, string replacementString)
    {
        string output = original;

        if (output.IsAPartialMatch(toBeReplaced))
        {
            string tempOutput = output.ToLower().Replace(toBeReplaced.ToLower(), replacementString);

            for(int index = 0; index < tempOutput.Length; index++)
            {
                string currentTemp = tempOutput[index].ToString().ToLower();
                string current = output[index].ToString().ToLower();
                
                if (currentTemp.Equals(current))
                {
                    if (output[index].IsUpperCaseLetter())
                    {
                        tempOutput = tempOutput.Insert(index, current.ToUpper());
                        tempOutput = tempOutput.Remove(index + 1, 1);
                    }
                }
            }
            
            output = tempOutput;
        }
        
        return output;
    }

    /// <summary>
    /// Replaces partial matches of a specified string in an IEnumerable of strings with a specified replacement string.
    /// </summary>
    /// <param name="enumerable">The IEnumerable of strings to be searched.</param>
    /// <param name="toBeReplaced">The string to be replaced.</param>
    /// <param name="replacementString">The replacement string.</param>
    /// <returns>The modified IEnumerable of strings if the string to be replaced was found; The unmodified original IEnumerable otherwise.</returns>
    public IEnumerable<string> ReplacePartialMatch(IEnumerable<string> enumerable, string toBeReplaced, string replacementString)
    {
        List<string> output = new();

        foreach (string str in enumerable)
        {
            string tempStr = str;
            
            tempStr = ReplacePartialMatch(tempStr, toBeReplaced, replacementString);
            
            output.Add(tempStr);
        }

        return output;
    }
}