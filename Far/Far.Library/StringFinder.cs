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
using System.IO;
using System.Threading.Tasks;

using Far.Library.Abstractions;
using Far.Library.Extensions;
using Far.Library.Models;

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

    public SearchResult FindStrings(IEnumerable<string> contentsToBeSearched, string s)
    {
        throw new System.NotImplementedException();
    }

    public Task<SearchResult> FindStringsAsync(IEnumerable<string> contentsToBeSearched, string s)
    {
        throw new System.NotImplementedException();
    }
    
    public SearchResult FindStringsInFile(string filePath, string s)
    {
        throw new System.NotImplementedException();
    }

    public Task<SearchResult> FindStringsInFileAsync(string filePath, string s)
    {
        throw new System.NotImplementedException();
    }

    public bool TryFindStrings(IEnumerable<string> contentsToBeSearched, string s, out SearchResult result)
    {
        throw new System.NotImplementedException();
    }
    
    public Task<bool> TryFindStringsAsync(IEnumerable<string> contentsToBeSearched, string s, out SearchResult result)
    {
        throw new System.NotImplementedException();
    }

    public bool TryFindInFile(string filePath, string s, out SearchResult result)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> TryFindInFileAsync(string filePath, string s, out SearchResult result)
    {
        throw new System.NotImplementedException();
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
    
}