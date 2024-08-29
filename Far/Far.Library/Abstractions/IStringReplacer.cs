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
using Far.Library.Models;

namespace Far.Library.Abstractions;

public interface IStringReplacer
{
    public string Replace(string original, StringPosition itemPositionToReplace, string replacement);
    public string Replace(string original, SearchResultItem itemToBeReplaced, string replacement);
    public IEnumerable<string> Replace(IEnumerable<string> enumerable, SearchResultItem itemToBeReplaced, string replacement);
    
    public string ReplaceCharacter(string original, char toBeReplaced, char replacementChar);

    public IEnumerable<string> ReplaceCharacter(IEnumerable<string> enumerable, char toBeReplaced, char replacementChar);

    public string ReplaceExactMatch(string original, string toBeReplaced, string replacementString);

    public IEnumerable<string> ReplaceExactMatch(IEnumerable<string> enumerable, string toBeReplaced,
        string replacementString);

    public string ReplacePartialMatch(string original, string toBeReplaced, string replacementString);

    public IEnumerable<string> ReplacePartialMatch(IEnumerable<string> enumerable, string toBeReplaced,
        string replacementString);
}