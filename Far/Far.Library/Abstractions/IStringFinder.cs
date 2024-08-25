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

using System.Collections.Generic;

namespace Far.Library.Abstractions;

public interface IStringFinder
{
    public bool ContainsPartialMatch(IEnumerable<string> contentsToBeSearched, char c);
    public bool ContainsPartialMatch(IEnumerable<string> contentsToBeSearched, string s);
    
    public bool ContainsExactMatch(IEnumerable<string> contentsToBeSearched, string s);

    public (IEnumerable<string> exactMatches, IEnumerable<string> partialMatches) Find(
        IEnumerable<string> contentsToBeSearched, string s);

    public bool TryFind(IEnumerable<string> contentsToBeSearched, string s,
        out IEnumerable<string> exactMatches, out IEnumerable<string> partialMatches);

}