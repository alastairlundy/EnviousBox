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

using Far.Library.Abstractions;
using Far.Library.Extensions;

namespace Far.Library;

public class StringFinder : IStringFinder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="contentsToBeSearched"></param>
    /// <param name="s">The string to search for.</param>
    /// <returns></returns>
    public bool ContainsExactMatch(IEnumerable<string> contentsToBeSearched, string s)
    {
        foreach (string contentLine in contentsToBeSearched)
        {
            if (contentLine.Equals(s))
            {
                return true;
            }
            
            foreach (string word in contentLine.Split(' '))
            {
                if (word.Equals(s))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contentsToBeSearched"></param>
    /// <param name="s">The string to search for.</param>
    /// <returns></returns>
    public bool ContainsPartialMatch(IEnumerable<string> contentsToBeSearched, string s)
    {
        foreach (string contentLine in contentsToBeSearched)
        {
            if (contentLine.IsAPartialMatch(s))
            {
                return true;
            }

            foreach (string word in contentLine.Split(' '))
            {
                if (word.IsAPartialMatch(s))
                {
                    return true;
                } 
            }
        }

        return false;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="contentsToBeSearched"></param>
    /// <param name="c">The char to search for.</param>
    /// <returns>true if a partial match</returns>
    public bool ContainsPartialMatch(IEnumerable<string> contentsToBeSearched, char c)
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contentsToBeSearched"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public (IEnumerable<string> exactMatches, IEnumerable<string> partialMatches) Find(IEnumerable<string> contentsToBeSearched, string s)
    {
        List<string> exactMatches = new List<string>();
        List<string> partialMatches = new List<string>();
        
        foreach (string str in contentsToBeSearched)
        {
            if (str.Split(' ').Length > 0)
            {
                foreach (string word in str.Split(' '))
                {
                    if (word.IsAPartialMatch(s) && !word.Equals(s))
                    {
                        partialMatches.Add(word);
                    }
                    else if (word.Equals(s))
                    {
                        exactMatches.Add(word);
                    }
                }
            }

            if (str.IsAPartialMatch(s) && !str.Equals(s))
            {
                partialMatches.Add(str);
            }
            else if (str.Equals(s))
            {
                exactMatches.Add(s);
            }
        }

        return (exactMatches, partialMatches);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contentsToBeSearched"></param>
    /// <param name="s"></param>
    /// <param name="exactMatches"></param>
    /// <param name="partialMatches"></param>
    /// <returns></returns>
    public bool TryFind(IEnumerable<string> contentsToBeSearched, string s, out IEnumerable<string> exactMatches, out IEnumerable<string> partialMatches)
    {
        List<string> foundExactMatches = new List<string>();
        List<string> foundPartialMatches = new List<string>();
        
        foreach (string str in contentsToBeSearched)
        {
            if (str.Split(' ').Length > 0)
            {
                foreach (string word in str.Split(' '))
                {
                    if (word.IsAPartialMatch(s) && !word.Equals(s))
                    {
                        foundPartialMatches.Add(word);
                    }
                    else if (word.Equals(s))
                    {
                        foundExactMatches.Add(word);
                    }
                }
            }

            if (str.IsAPartialMatch(s) && !str.Equals(s))
            {
                foundPartialMatches.Add(str);
            }
            else if (str.Equals(s))
            {
                foundExactMatches.Add(s);
            }
        }

        exactMatches = foundExactMatches.ToArray();
        partialMatches = foundPartialMatches.ToArray();

        if (foundExactMatches.Count > 0 || foundPartialMatches.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}