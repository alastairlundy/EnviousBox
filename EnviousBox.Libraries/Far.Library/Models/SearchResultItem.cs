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

namespace Far.Library.Models;

public class SearchResultItem : IEquatable<SearchResultItem>
{
    public SearchResultItem()
    {
        ResultPositions = new List<StringPosition>();
        ResultValue = string.Empty;
    }

    public SearchResultItem(List<StringPosition> resultPositions, string resultValue)
    {
        this.ResultPositions = resultPositions;
        this.ResultValue = resultValue;
    }
    
    public List<StringPosition> ResultPositions { get; set; }
    
    public string ResultValue { get; set;  }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(SearchResultItem? other)
    {
        if (other is null)
        {
            return false;
        }
        else
        {
            return ResultPositions.Equals(other.ResultPositions) && ResultValue.Equals(other.ResultValue);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is SearchResultItem item)
        {
            return Equals(item);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ResultPositions, ResultValue);
    }
}