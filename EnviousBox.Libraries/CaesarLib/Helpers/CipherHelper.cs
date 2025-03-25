/*
    EnviousBox - Caesar Library
    Copyright (C) 2024-2025 Alastair Lundy

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

namespace AlastairLundy.CaesarLib.Helpers
{
    internal static class CipherHelper
    {
        internal static string Alphabet { get;  }= "abcdefghijklmnopqrstuvwxyz";

    
        internal static int GetAlphabetPosition(char c)
        {
            for (int index = 0; index < Alphabet.Length; index++)
            {
                if (Alphabet[index].ToString().Equals(c.ToString().ToLower()))
                {
                    return index;
                }
            }

            throw new ArgumentException("The value provided was not in the alphabet.");
        }

        internal static int OldPositionCalculator(int newPosition, int shift)
        {
            if (newPosition - shift > 0 && newPosition - shift < 26)
            {
                return newPosition - shift;
            }
            else
            {
                int xMoreThan25 = 0;
            
                if (shift > 25)
                {
                    xMoreThan25 = shift % 25;
                }
                shift -= (xMoreThan25 * shift);
            
                return newPosition - shift;
            }
        }
    
        internal static int NewPositionCalculator(int oldPosition, int shift)
        {
            if (oldPosition + shift < 26)
            {
                return oldPosition + shift;
            }
            else
            {
                int xMoreThan25 = 0;
            
                if (shift > 25)
                {
                    xMoreThan25 = shift % 25;
                }

                if (xMoreThan25 > 1)
                {
                    shift -= (xMoreThan25 * shift);
                }
            
                return oldPosition + shift;
            }
        }

    }
}