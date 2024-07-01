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

using System.Diagnostics.Tracing;

namespace Far.Library;

public class StringFinder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="contentsToBeSearched"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool ContainsExactMatch(IEnumerable<string> contentsToBeSearched, char c)
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

    public static bool ContainsExactMatch(IEnumerable<string> contentsToBeSearched, string s)
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

    public static bool ContainsPartialMatch(IEnumerable<string> contentsToBeSearched, string s)
    {
        foreach (string contentLine in contentsToBeSearched)
        {
            if (IsAPartialMatch(contentLine, s))
            {
                return true;
            }

            foreach (string word in contentLine.Split(' '))
            {
                if (IsAPartialMatch(word, s))
                {
                    return true;
                } 
            }
        }

        return false;
    }
    
    public static bool ContainsPartialMatch(IEnumerable<string> contentsToBeSearched, char c)
    {
        foreach (string contentLine in contentsToBeSearched)
        {
            foreach (string word in contentLine.Split(' '))
            {
                foreach (char character in word)
                {
                    if (IsAPartialMatch(word, c) || character.Equals(c))
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
    /// <param name="toBeSearched"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool IsAPartialMatch(string toBeSearched, char c)
    {
        return (toBeSearched.ToLower().Equals(c.ToString().ToLower()) ||
                toBeSearched.ToLower().Contains(c.ToString().ToLower()));
    }
    
    public static bool IsAPartialMatch(string toBeSearched, string s)
    {
        return (toBeSearched.ToLower().Equals(s.ToLower()) || toBeSearched.ToLower().Contains(s.ToLower()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contentsToBeSearched"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static (IEnumerable<string> exactMatches, IEnumerable<string> partialMatches) Find(IEnumerable<string> contentsToBeSearched, string s)
    {
        List<string> exactMatches = new List<string>();
        List<string> partialMatches = new List<string>();
        
        foreach (string str in contentsToBeSearched)
        {
            if (str.Split(' ').Length > 0)
            {
                foreach (string word in str.Split(' '))
                {
                    if (IsAPartialMatch(word, s) && !word.Equals(s))
                    {
                        partialMatches.Add(word);
                    }
                    else if (word.Equals(s))
                    {
                        exactMatches.Add(word);
                    }
                }
            }

            if (IsAPartialMatch(str, s) && !str.Equals(s))
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
    public static bool TryFind(IEnumerable<string> contentsToBeSearched, string s, out IEnumerable<string> exactMatches, out IEnumerable<string> partialMatches)
    {
        List<string> foundExactMatches = new List<string>();
        List<string> foundPartialMatches = new List<string>();
        
        foreach (string str in contentsToBeSearched)
        {
            if (str.Split(' ').Length > 0)
            {
                foreach (string word in str.Split(' '))
                {
                    if (IsAPartialMatch(word, s) && !word.Equals(s))
                    {
                        foundPartialMatches.Add(word);
                    }
                    else if (word.Equals(s))
                    {
                        foundExactMatches.Add(word);
                    }
                }
            }

            if (IsAPartialMatch(str, s) && !str.Equals(s))
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