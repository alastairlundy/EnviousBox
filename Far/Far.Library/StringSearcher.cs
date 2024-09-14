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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AlastairLundy.Extensions.Collections.IEnumerables;

using Far.Library.Abstractions;
using Far.Library.Models;

using AlastairLundy.Extensions.System;

using AlastairLundy.Extensions.System.Matching;

namespace Far.Library;

public class StringSearcher : IStringSearcher
{
    protected IStringPositionFinder stringPositionFinder;

    public StringSearcher()
    {
        stringPositionFinder = new StringPositionFinder();
    }
    
    public StringSearcher(IStringPositionFinder stringPositionFinder)
    {
        this.stringPositionFinder = stringPositionFinder; 
    }
    
    /// <summary>
    /// Returns whether an IEnumerable of strings contains an exact match of a specified string.
    /// </summary>
    /// <param name="contentsToBeSearched">The IEnumerable to be searched.</param>
    /// <param name="s">The string to search for.</param>
    /// <returns>True if a case-sensitive exact match is found; False otherwise.</returns>
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
    /// Searches a string for a specified string and returns the results of the search with any partial and exact matches.
    /// </summary>
    /// <param name="contentsToBeSearched">The string to be searched.</param>
    /// <param name="s">The specified string to search for.</param>
    /// <returns>The result of the search including any Exact and Partial matches if found.</returns>
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
    /// Searches an IEnumerable of strings for a specified string and returns the results of the search with any partial and exact matches.
    /// </summary>
    /// <param name="contentsToBeSearched">The IEnumerable to be searched.</param>
    /// <param name="s">The specified string to search for.</param>
    /// <returns>The result of the search including any Exact and Partial matches if found.</returns>
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
    /// Searches a file for a specified string and returns the results of the search with any partial and exact matches.
    /// </summary>
    /// <param name="filePath">The file path of the file to be searched.</param>
    /// <param name="s">The string to search for.</param>
    /// <returns>The result of the search including any Exact and Partial matches if found.</returns>
    public SearchResult FindStringsInFile(string filePath, string s)
    {
        string[] fileContents = File.ReadAllLines(filePath);
        
        return FindStrings(fileContents, s);
    }

    /// <summary>
    /// Asynchronously reads a file, and synchronously searches the file contents for a specified string and returns the results of the search with any partial and exact matches.
    /// </summary>
    /// <param name="filePath">The file path of the file to search.</param>
    /// <param name="s">The string to search for.</param>
    /// <returns>The result of the search including any Exact and Partial matches if found.</returns>
    public async Task<SearchResult> FindStringsInFileAsync(string filePath, string s)
    {
        string[] lines = await File.ReadAllLinesAsync(filePath);

        return FindStrings(lines, s);
    }

    /// <summary>
    /// Attempts to search for a specified string within a string.
    /// </summary>
    /// <param name="contentsToBeSearched">The string to be searched.</param>
    /// <param name="s">The string to search for.</param>
    /// <param name="result">The search results if the attempted search was completed; Null otherwise</param>
    /// <returns>>True if the attempted search was completed; False otherwise.</returns>
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
    /// Attempts to search for a specified string within an IEnumerable of strings.
    /// </summary>
    /// <param name="contentsToBeSearched">The IEnumerable to be searched.</param>
    /// <param name="s">The string to search for.</param>
    /// <param name="result">The search results if the attempted search was completed; Null otherwise</param>
    /// <returns>>True if the attempted search was completed; False otherwise.</returns>
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
    /// Attempts to search for a specified string within a file.
    /// </summary>
    /// <param name="filePath">The file to be searched.</param>
    /// <param name="s">The string to search for.</param>
    /// <param name="result">The search results if the attempted search was completed; Null otherwise</param>
    /// <returns>>True if the attempted search was completed; False otherwise.</returns>
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
    /// Determines whether a partial match to a string is found within an IEnumerable of string.
    /// </summary>
    /// <param name="contentsToBeSearched">The IEnumerable to be searched.</param>
    /// <param name="s">The string to search for.</param>
    /// <returns>True if a case-insensitive partial match is found; False otherwise.</returns>
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