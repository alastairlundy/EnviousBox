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

public class CharPosition : IEquatable<CharPosition>
{
    public CharPosition()
    {
        LineNumber = 0;
        ColumnNumber = 0;
    }

    public CharPosition(int lineNumber, int columnNumber)
    {
        this.LineNumber = lineNumber;
        this.ColumnNumber = columnNumber;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public int LineNumber { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int ColumnNumber { get; set; }

    /// <summary>
    /// Determines whether the object provided is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to be compared.</param>
    /// <returns>true if the object provided is equal to the current object; false otherwise.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }
        
        if (obj is CharPosition position)
        {
            return Equals(position);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Returns the hashcode for this object.
    /// </summary>
    /// <returns>A 32-Bit signed integer hashcode.</returns>
    public override int GetHashCode()
    {
        string hash = $"LN{LineNumber}CN{ColumnNumber}";
        
        return hash.GetHashCode();
    }

    /// <summary>
    /// Determines whether the TextCaretPosition provided is equal to the current object.
    /// </summary>
    /// <param name="other">The object to be compared.</param>
    /// <returns>true if the object provided is equal to the current object; false otherwise.</returns>
    public bool Equals(CharPosition? other)
    {
        if (other is not null)
        {
            return (this.LineNumber == other.LineNumber) && (this.ColumnNumber == other.ColumnNumber);
        }
        else
        {
            return false;
        }
    }
}