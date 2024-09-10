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

namespace Far.Library.Extensions;

public static class PartialMatchExtensions
{
    /// <summary>
    /// Determines whether a string is or contains a partial match to another string.
    /// </summary>
    /// <param name="toBeSearched">The string to be searched.</param>
    /// <param name="s">The string to look for.</param>
    /// <returns>true if a partial match to a string is found within the string to be searched; false otherwise.</returns>
    public static bool IsAPartialMatch(this string toBeSearched, string s)
    {
        return (toBeSearched.ToLower().Equals(s.ToLower()) || toBeSearched.ToLower().Contains(s.ToLower()));
    }
}