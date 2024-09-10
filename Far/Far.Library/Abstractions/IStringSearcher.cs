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

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using Far.Library.Models;

namespace Far.Library.Abstractions;

public interface IStringSearcher
{
    public bool ContainsPartialMatch(IEnumerable<string> contentsToBeSearched, string s);
    
    public bool ContainsExactMatch(IEnumerable<string> contentsToBeSearched, string s);

    public SearchResult FindStrings(string contentsToBeSearched, string s);
    
    public SearchResult FindStrings(IEnumerable<string> contentsToBeSearched, string s);
    public SearchResult FindStringsInFile(string filePath, string s);
    public Task<SearchResult> FindStringsInFileAsync(string filePath, string s);
    
    
    public bool TryFindStrings(string contentsToBeSearched, string s, out SearchResult? result);
    public bool TryFindStrings(IEnumerable<string> contentsToBeSearched, string s, out SearchResult? result);
    public bool TryFindInFile(string filePath, string s, out SearchResult? result);

}