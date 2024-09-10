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