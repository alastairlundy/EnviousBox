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

using System.Collections.Generic;

namespace Far.Library.Extensions;

public static class ContainsCharExtensions
{
    /// <summary>
    /// Determines if an IEnumerable of strings contains a char.
    /// </summary>
    /// <param name="contentsToBeSearched">The IEnumerable of strings to be searched.</param>
    /// <param name="c">The character to search for.</param>
    /// <returns>true if the IEnumerable of strings contains the specified char</returns>
    public static bool ContainsChar(this IEnumerable<string> contentsToBeSearched, char c)
    {
        foreach (string contentLine in contentsToBeSearched)
        {
            foreach (string word in contentLine.Split(' '))
            {
                foreach (char character in word)
                {
                    if (character.Equals(c))
                    {
                        return true;
                    }
                }
            }
        }
    
        return false;
    }
}