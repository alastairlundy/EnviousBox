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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.Extensions.Collections.Specializations.Indexes;
using AlastairLundy.Extensions.System.Indexes;

using Far.Library.Abstractions;

namespace Far.Library;

public class StringIndexFinder : IStringIndexFinder
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="toBeSearched"></param>
    /// <param name="expected"></param>
    /// <param name="ignoreCase"></param>
    /// <returns></returns>
    public IEnumerable<int> GetStringIndexes(string toBeSearched, string expected, bool ignoreCase)
    {
        return toBeSearched.IndexesOf(expected, ignoreCase);
    }

    public IEnumerable<int> GetStringIndexes(IEnumerable<string> toBeSearched, string expected, bool ignoreCase)
    {
        return toBeSearched.StringIndexesOf(expected, ignoreCase);
    }

    public IEnumerable<int> GetCharIndexes(IEnumerable<string> toBeSearched, char expected, bool ignoreCase)
    {
        return toBeSearched.CharIndexesOf(expected, ignoreCase);
    }


    /// <summary>
    /// Gets the indexes of the specified char in a string.
    /// </summary>
    /// <param name="toBeSearched">The string to be searched.</param>
    /// <param name="expected">The char to look for.</param>
    /// <param name="ignoreCase">Whether to ignore the case of the expected char.</param>
    /// <returns>The indexes if the char is found.</returns>
    public IEnumerable<int> GetCharIndexes(string toBeSearched, char expected, bool ignoreCase)
    {
        return toBeSearched.IndexesOf(expected, ignoreCase);
    }
}