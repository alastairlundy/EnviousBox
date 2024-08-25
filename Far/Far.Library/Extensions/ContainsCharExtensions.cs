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