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
using System.Linq;

namespace Far.Library.Models;

public class SearchResult : IEquatable<SearchResult>
{

    public SearchResult(IEnumerable<SearchResultItem> ExactMatches, IEnumerable<SearchResultItem> PartialMatches)
    {
        this.ExactMatches = ExactMatches;
        this.PartialMatches = PartialMatches;
    }

    public IEnumerable<SearchResultItem> ExactMatches { get; protected set; }

    public IEnumerable<SearchResultItem> PartialMatches { get; protected set; }

    public int NumberOfExactMatches
    {
        get
        {
            try
            {
                return ExactMatches.Count();
            }
            catch
            {
                return 0;
            }
        }
    }

    public int NumberOfPartialMatches
    {
        get
        {
            try
            {
                return PartialMatches.Count();
            }
            catch
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(SearchResult? other)
    {
        if (other is null)
        {
            return false;
        }
        else
        {
            return this.ExactMatches.SequenceEqual(other.ExactMatches) && this.PartialMatches.SequenceEqual(other.PartialMatches);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not SearchResult)
        {
            return false;
        }
        else if (obj is SearchResult searchResult)
        {
            return Equals(searchResult);
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ExactMatches, PartialMatches);
    }
}