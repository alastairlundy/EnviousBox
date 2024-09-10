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

namespace Far.Library.Models;

public class StringPosition : IEquatable<StringPosition>
{
    public StringPosition()
    {
        StartPosition = new CharPosition();
        EndPosition = new CharPosition();
    }

    public StringPosition(CharPosition startPosition, CharPosition endPosition)
    {
        StartPosition = startPosition;
        EndPosition = endPosition;
    }
    
    public CharPosition StartPosition { get; set; }
    public CharPosition EndPosition { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(StringPosition? other)
    {
        if (other is null)
        {
            return false;
        }
        else
        {
            return StartPosition.Equals(other.StartPosition) && EndPosition.Equals(other.EndPosition);
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

        if (obj is StringPosition position)
        {
            return Equals(position);
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(StartPosition.GetHashCode(), EndPosition.GetHashCode());
    }
}