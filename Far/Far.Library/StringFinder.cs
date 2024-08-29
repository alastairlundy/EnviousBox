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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AlastairLundy.Extensions.Collections.IEnumerables;

using Far.Library.Abstractions;
using Far.Library.Extensions;
using Far.Library.Models;

using AlastairLundy.Extensions.System;

namespace Far.Library;

public class StringFinder : IStringFinder
{
    protected StringPositionFinder stringPositionFinder;

    public StringFinder()
    {
        stringPositionFinder = new StringPositionFinder();
    }
    
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
    /// <param name="s"></param>
    /// <returns></returns>
    public SearchResult FindStrings(string contentsToBeSearched, string s)
    {
        List<SearchResultItem> exactMatches = new List<SearchResultItem>();
        List<SearchResultItem> partialMatches = new List<SearchResultItem>();
            
        if (contentsToBeSearched.ContainsSpaceSeparatedSubStrings() == true)
        {
            string[] spaceSeparatedStrings = contentsToBeSearched.Split(' ');

            foreach (string str in spaceSeparatedStrings)
            {
                if (ContainsExactMatch(new string[] { str }, s) == true)
                {
                    exactMatches.Add(new SearchResultItem
                    {
                        ResultValue = str,
                        ResultPositions = stringPositionFinder.GetStringPositions(str, s, false).ToList(),
                    });
                }
                else if (ContainsPartialMatch(new string[] { str }, s) == true)
                {
                    partialMatches.Add(new SearchResultItem
                    {
                        ResultValue = str,
                        ResultPositions = stringPositionFinder.GetStringPositions(str, s, false).ToList()
                    });
                }
            }
        }
        else
        {
            if (ContainsExactMatch(new string[] { contentsToBeSearched }, s))
            {
                exactMatches.Add(new SearchResultItem
                {
                    ResultValue = contentsToBeSearched,
                    ResultPositions = stringPositionFinder.GetStringPositions(contentsToBeSearched, s, false).ToList()
                });
            }
            else if (ContainsPartialMatch(new string[] { contentsToBeSearched }, s))
            {
                partialMatches.Add(new SearchResultItem
                {
                    ResultValue = contentsToBeSearched,
                    ResultPositions = stringPositionFinder.GetStringPositions(contentsToBeSearched, s, true).ToList()
                });
            }
        }
        
        SearchResult output = new SearchResult(exactMatches, partialMatches);
        return output;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contentsToBeSearched"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public SearchResult FindStrings(IEnumerable<string> contentsToBeSearched, string s)
    {
        List<SearchResultItem> exactMatches = new List<SearchResultItem>();
        List<SearchResultItem> partialMatches = new List<SearchResultItem>();

        foreach (string content in contentsToBeSearched)
        {
            SearchResult result = FindStrings(content, s);

           exactMatches = exactMatches.Combine(result.ExactMatches.ToList()).ToList();
           partialMatches = partialMatches.Combine(result.PartialMatches.ToList()).ToList();
        }
        
        SearchResult output = new SearchResult(exactMatches, partialMatches);
        return output;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="s">The string to search for.</param>
    /// <returns></returns>
    public SearchResult FindStringsInFile(string filePath, string s)
    {
        string[] fileContents = File.ReadAllLines(filePath);
        
        return FindStrings(fileContents, s);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath">The file path of the file to search.</param>
    /// <param name="s">The string to search for.</param>
    /// <returns></returns>
    public async Task<SearchResult> FindStringsInFileAsync(string filePath, string s)
    {
        string[] lines = await File.ReadAllLinesAsync(filePath);

        return FindStrings(lines, s);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contentsToBeSearched"></param>
    /// <param name="s"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool TryFindStrings(string contentsToBeSearched, string s, out SearchResult? result)
    {
        try
        {
            result = FindStrings(contentsToBeSearched, s);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contentsToBeSearched"></param>
    /// <param name="s"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool TryFindStrings(IEnumerable<string> contentsToBeSearched, string s, out SearchResult? result)
    {
        try
        {
            result = FindStrings(contentsToBeSearched, s);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="s"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool TryFindInFile(string filePath, string s, out SearchResult? result)
    {
        try
        {
            result = FindStringsInFile(filePath, s);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
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