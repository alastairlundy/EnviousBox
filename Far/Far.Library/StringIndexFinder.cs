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

using System;
using System.Collections.Generic;

using Far.Library.Abstractions;

namespace Far.Library;

public class StringIndexFinder : IStringIndexFinder
{
    /// <summary>
    /// Gets the indexes of the specified string within a string.
    /// </summary>
    /// <param name="strings">The IEnumerable of strings to be searched.</param>
    /// <param name="expected">The string to look for.</param>
    /// <param name="ignoreCase">Whether to ignore the case of the expected string.</param>
    /// <returns>The indexes if the string is found.</returns>
    public IEnumerable<int> GetStringIndex(IEnumerable<string> strings, string expected, bool ignoreCase)
    {
        List<int> indexes = new List<int>();

        int currentCharIndex = 0;
        
        foreach (string str in strings)
        {
            if (str.Split(' ').Length > 0)
            {
                string[] words = str.Split(' ');

                foreach (string word in words)
                {
                    if(word.Equals(expected, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                    {
                        indexes.Add(currentCharIndex);
                    }

                    foreach (char unused in word)
                    {
                        currentCharIndex++;
                    }
                }
            }
            else
            {
                if (str.Contains(expected))
                {
                    if (ignoreCase)
                    {
                        indexes.Add(currentCharIndex + str.IndexOf(expected, StringComparison.OrdinalIgnoreCase));  
                    }
                    else
                    {
                        indexes.Add(currentCharIndex + str.IndexOf(expected, StringComparison.Ordinal));
                    }
                }
                
                foreach (char unused in str)
                {
                    currentCharIndex++;
                }
            }
        }
        
        return indexes;
    }

    /// <summary>
    /// Gets the indexes of the specified string within a string.
    /// </summary>
    /// <param name="toBeSearched">The string to be searched.</param>
    /// <param name="expected">The string to look for.</param>
    /// <param name="ignoreCase">Whether to ignore the case of the expected string.</param>
    /// <returns>The indexes if the string is found.</returns>
    public IEnumerable<int> GetStringIndex(string toBeSearched, string expected, bool ignoreCase)
    {
        return GetStringIndex(new string[]{toBeSearched}, expected, ignoreCase);
    }
    
    /// <summary>
    /// Gets the indexes of the specified char in an IEnumerable of strings.
    /// </summary>
    /// <param name="strings">The IEnumerable of strings to be searched.</param>
    /// <param name="expected">The char to look for.</param>
    /// <param name="ignoreCase">Whether to ignore the case of the expected char.</param>
    /// <returns>The indexes if the char is found.</returns>
    public IEnumerable<int> GetCharIndex(IEnumerable<string> strings, char expected, bool ignoreCase)
    {
        List<int> indexes = new List<int>();

        int currentIndex = 0;

        foreach (string str in strings)
        {
            foreach (char c in str)
            {
                if (c == expected)
                {
                    if (ignoreCase ||
                        string.Equals(c.ToString(), expected.ToString(), StringComparison.InvariantCulture) ||
                        string.Equals(c.ToString(), expected.ToString(), StringComparison.InvariantCulture))
                    {
                        indexes.Add(currentIndex);
                    }
                }
            
                currentIndex++;
            }
        }
        
        return indexes;
    }
    
    /// <summary>
    /// Gets the indexes of the specified char in a string.
    /// </summary>
    /// <param name="toBeSearched">The string to be searched.</param>
    /// <param name="expected">The char to look for.</param>
    /// <param name="ignoreCase">Whether to ignore the case of the expected char.</param>
    /// <returns>The indexes if the char is found.</returns>
    public IEnumerable<int> GetCharIndex(string toBeSearched, char expected, bool ignoreCase)
    {
       return GetCharIndex(new string[] { toBeSearched }, expected, ignoreCase);
    }
}