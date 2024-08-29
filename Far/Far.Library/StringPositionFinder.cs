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
using System.Linq;

using AlastairLundy.Extensions.Collections.IEnumerables;

using Far.Library.Abstractions;
using Far.Library.Models;

namespace Far.Library;

/// <summary>
/// 
/// </summary>
public class StringPositionFinder : IStringPositionFinder
{
    protected IEnumerable<CharPosition> SearchStringForChar(string input, char expected, int lineNumber, bool ignoreCase)
    {
        List<CharPosition> positions = new List<CharPosition>();
        positions.TrimExcess();
        
        for(int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            
            if (ignoreCase)
            {
                if (c.ToString().ToLower() == expected.ToString().ToLower())
                {
                    positions.Add(new CharPosition(lineNumber, i));
                }
            }
            else
            {
                if (c == expected)
                {
                    positions.Add(new CharPosition(lineNumber, i));
                }
            }
        }
        
        return positions;
    }
    
    /// <summary>
    /// Gets the positions of a character within a string if the character is found.
    /// </summary>
    /// <param name="toBeSearched">The string to be searched.</param>
    /// <param name="expected">The character to look for.</param>
    /// <param name="ignoreCase">Whether to ignore the case of the character being looked for.</param>
    /// <returns>an IEnumerable of char positions if the char is found within the string; an empty IEnumerable otherwise.</returns>
    public IEnumerable<CharPosition> GetCharPositions(string toBeSearched, char expected, bool ignoreCase)
    {
        List<CharPosition> positions = new List<CharPosition>();
        positions.TrimExcess();

        if (toBeSearched.Contains(expected))
        {
            string[] lines = toBeSearched.Split(Environment.NewLine);

            if (lines.Length > 0)
            {
                int lineCount = 0;

                foreach (string line in lines)
                {
                    positions = positions.Combine(SearchStringForChar(line, expected, lineCount, ignoreCase)).ToList();
                }
            }
            else
            {
                positions = positions.Combine(SearchStringForChar(toBeSearched, expected, 0, ignoreCase)).ToList();
            }
        }

        return positions;
    }
    
    /// <summary>
    /// Gets the positions of a character within an IEnumerable of strings if the character is found.
    /// </summary>
    /// <param name="strings">The IEnumerable of strings to be searched.</param>
    /// <param name="expected">The character to look for.</param>
    /// <param name="ignoreCase">Whether to ignore the case of the character being looked for.</param>
    /// <returns>an IEnumerable of char positions if the char is found within the IEnumerable of strings; an empty IEnumerable otherwise.</returns>
    public IEnumerable<CharPosition> GetCharPositions(IEnumerable<string> strings, char expected, bool ignoreCase)
    {
        List<CharPosition> charPositions = new();
        charPositions.TrimExcess();
        
        int lineCount = 0;
        
        foreach (string str in strings)
        {
            string[] lines = str.Split(Environment.NewLine);
            
            foreach (string line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                   charPositions = charPositions.Combine(SearchStringForChar(line, expected, lineCount, ignoreCase)).ToList();
                }
                
                lineCount++;
            }
        }

        return charPositions;
    }

    protected IEnumerable<StringPosition> SearchStringForString(string str, string expected, bool ignoreCase, int lineNumber)
    {
        List<StringPosition> positions = new();

        string[] words = str.Split(' ');

        foreach (string word in words)
        {
            if (word.Contains(expected))
            {
                int column;

                if (ignoreCase == true)
                {
                    column = str.IndexOf(expected, StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    column = str.IndexOf(expected, StringComparison.Ordinal);
                }

                CharPosition startPosition = new(lineNumber, column);
                CharPosition endPosition;

                for (int i = column + 1; i < str.Length; i++)
                {
                    if (str[i] == ' ')
                    {
                        endPosition = new CharPosition(lineNumber, i - 1);

                        positions.Add(new StringPosition
                        {
                            EndPosition = endPosition,
                            StartPosition = startPosition,
                        });

                        break;
                    }
                }
            }
        }
        
        return positions;
    }

    /// <summary>
    /// Gets the positions of a string within a larger string if the string is found.
    /// </summary>
    /// <param name="stringToBeSearched">The string to be searched.</param>
    /// <param name="expected">The string to look for.</param>
    /// <param name="ignoreCase">Whether to ignore the case of the string being looked for.</param>
    /// <returns>an IEnumerable of string positions if the string is found within the specified string; an empty IEnumerable otherwise.</returns>
    public IEnumerable<StringPosition> GetStringPositions(string stringToBeSearched, string expected, bool ignoreCase)
    {
        List<StringPosition> stringPositions = new();

        if (stringToBeSearched.Split(Environment.NewLine).Length > 0)
        {
            string[] lines = stringToBeSearched.Split(Environment.NewLine);

            int lineCount = 0;
            
            foreach (string line in lines)
            {
                if (line.Contains(expected))
                {
                   stringPositions = stringPositions.Combine(SearchStringForString(line, expected, ignoreCase, lineCount).ToList()).ToList();
                }

                lineCount++;
            }
        }
        else
        {
            stringPositions = SearchStringForString(stringToBeSearched, expected, ignoreCase, 0).ToList();
        }

        return stringPositions;
    }
    
    /// <summary>
    /// Gets the positions of a string within an IEnumerable of strings if the string is found.
    /// </summary>
    /// <param name="strings">The IEnumerable of strings to be searched.</param>
    /// <param name="expected">The string to look for.</param>
    /// <param name="ignoreCase">Whether to ignore the case of the string being looked for.</param>
    /// <returns>an IEnumerable of string positions if the string is found within the IEnumerable of strings; an empty IEnumerable otherwise.</returns>
    public IEnumerable<StringPosition> GetStringPositions(IEnumerable<string> strings, string expected, bool ignoreCase)
    {
        List<StringPosition> stringPositions = new();
        stringPositions.TrimExcess();
        
        foreach (string str in strings)
        {
            stringPositions = stringPositions.Combine(SearchStringForString(str, expected, ignoreCase, stringPositions.Count)).ToList(); 
        }
        
        return stringPositions;
    }
}