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