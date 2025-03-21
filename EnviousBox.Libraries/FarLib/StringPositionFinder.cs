﻿/*
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
using AlastairLundy.Extensions.Collections.Generic;

using AlastairLundy.FarLib.Abstractions;
using AlastairLundy.FarLib.Models;

namespace AlastairLundy.FarLib;

/// <summary>
/// 
/// </summary>
public class StringPositionFinder : IStringPositionFinder
{
    private readonly IStringIndexFinder _stringIndexFinder;

    public StringPositionFinder(IStringIndexFinder stringIndexFinder)
    {
        this._stringIndexFinder = stringIndexFinder;
    }

    protected IEnumerable<CharPosition> SearchForChar(string input, char expected, int lineNumber, bool ignoreCase)
    {
        List<CharPosition> positions = new List<CharPosition>();
        positions.TrimExcess();

        int[] indexes = _stringIndexFinder.GetCharIndexes(input, expected, ignoreCase).ToArray();

        foreach (int index in indexes)
        {
            positions.Add(new CharPosition(lineNumber, index));
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
                    positions = positions.Combine(SearchForChar(line, expected, lineCount, ignoreCase)).ToList();
                }
            }
            else
            {
                positions = positions.Combine(SearchForChar(toBeSearched, expected, 0, ignoreCase)).ToList();
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
                   charPositions = charPositions.Combine(SearchForChar(line, expected, lineCount, ignoreCase)).ToList();
                }
                
                lineCount++;
            }
        }

        return charPositions;
    }

    protected IEnumerable<StringPosition> SearchForString(string str, string expected, bool ignoreCase, int lineNumber)
    {
        List<StringPosition> positions = new();
        
        int[] indexes = _stringIndexFinder.GetStringIndexes(str, expected, ignoreCase).ToArray();

        if (indexes.Any() && indexes[0] != -1)
        {
            foreach (int index in indexes)
            {
                positions.Add(new StringPosition()
                {
                    EndPosition = new CharPosition()
                    {
                        LineNumber = lineNumber,
                        ColumnNumber = index + expected.Length,
                    },
                    StartPosition = new CharPosition()
                    {
                        LineNumber = lineNumber,
                        ColumnNumber = index
                    }
                });
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
                   stringPositions = stringPositions.Combine(SearchForString(line, expected, ignoreCase, lineCount).ToList()).ToList();
                }

                lineCount++;
            }
        }
        else
        {
            stringPositions = SearchForString(stringToBeSearched, expected, ignoreCase, 0).ToList();
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
            stringPositions = stringPositions.Combine(SearchForString(str, expected, ignoreCase, stringPositions.Count)).ToList(); 
        }
        
        return stringPositions;
    }
}