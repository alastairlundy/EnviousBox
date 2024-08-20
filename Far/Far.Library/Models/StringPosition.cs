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