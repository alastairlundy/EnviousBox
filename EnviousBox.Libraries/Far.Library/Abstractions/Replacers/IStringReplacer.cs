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
using Far.Library.Models;

namespace Far.Library.Abstractions.Replacers;

public interface IStringReplacer
{
    public string Replace(string original, StringPosition itemPositionToReplace, string replacement);
    public string Replace(string original, SearchResultItem itemToBeReplaced, string replacement);
    public IEnumerable<string> Replace(IEnumerable<string> enumerable, SearchResultItem itemToBeReplaced, string replacement);

    public string ReplacePartialMatch(string original, string toBeReplaced, string replacementString);

    public IEnumerable<string> ReplacePartialMatch(IEnumerable<string> enumerable, string toBeReplaced,
        string replacementString);
}