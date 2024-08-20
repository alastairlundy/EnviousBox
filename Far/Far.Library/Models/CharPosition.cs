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