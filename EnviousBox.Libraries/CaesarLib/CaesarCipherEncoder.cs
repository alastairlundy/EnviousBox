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
using System.Text;

using AlastairLundy.CaesarLib.Abstractions;
using AlastairLundy.CaesarLib.Helpers;

using AlastairLundy.Extensions.System.Strings;

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.CaesarLib
{
    public class CaesarCipherEncoder : ICaesarCipherEncoder
    {
        public char EncodeCharacter(char input, int shift)
        {
            char output;
            
            if (input.Equals(' '))
            {
                return input;
            }

            int newPosition = CipherHelper.NewPositionCalculator(CipherHelper.GetAlphabetPosition(input), shift);

            if (input.IsUpperCaseCharacter())
            {
                output = Convert.ToChar(CipherHelper
                    .Alphabet[newPosition]
                    .ToString().ToUpper());
            }
            else if (input.IsLowerCaseCharacter())
            {
                output = CipherHelper.Alphabet[newPosition];
            }
            else
            {
                return input;
            }
            
            return output;
        }

        public string EncodeString(string input, int shift)
        {
            StringBuilder stringBuilder = new StringBuilder();
        
            foreach (char c in input)
            {
                char character = EncodeCharacter(c, shift);
                
                stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }

        public StringSegment EncodeStringSegment(StringSegment input, int shift)
        { 
            string encodedString = EncodeString(input.Value, shift);
            
            return new StringSegment(encodedString);
        }
    }
}