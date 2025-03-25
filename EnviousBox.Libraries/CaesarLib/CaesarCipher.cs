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
using System.Collections.Generic;
using System.Text;

using AlastairLundy.CaesarLib.Helpers;
// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.CaesarLib
{
    /// <summary>
    /// A class that supports encoding and decoding text using the CaesarCipher.
    /// </summary>
    public class CaesarCipher
    {

        /// <summary>
        /// Encodes a normal string of latin alphabet text using the CaesarCipher, shifting each letter by a specified key.
        /// </summary>
        /// <param name="s">The string to be encoded.</param>
        /// <param name="shift">The amount to shift letters by.</param>
        /// <returns>The encoded string.</returns>
        public string Encode(string s, int shift)
        {
            StringBuilder stringBuilder = new StringBuilder();
        
            for (int characterIndex = 0; characterIndex < s.Length; characterIndex++)
            {
                char character = s[characterIndex];

                if (character.ToString().Equals(character.ToString().ToUpper()))
                {
                    character = Convert.ToChar(CipherHelper.Alphabet[CipherHelper.NewPositionCalculator(CipherHelper.GetAlphabetPosition(character), shift)].ToString().ToUpper());
                }
                else if (character.ToString().Equals(character.ToString().ToLower()))
                {
                    character = CipherHelper.Alphabet[CipherHelper.NewPositionCalculator(CipherHelper.GetAlphabetPosition(character), shift)];
                }

                stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }
    
        /// <summary>
        /// Encodes an IEnumerable of strings of latin alphabet text using the CaesarCipher, shifting each letter by a specified key.
        /// </summary>
        /// <param name="enumerable">The IEnumerable of strings to be encoded.</param>
        /// <param name="shift">The amount to shift letters by.</param>
        /// <returns>The encoded strings as an IEnumerable.</returns>
        public IEnumerable<string> Encode(IEnumerable<string> enumerable, int shift)
        {
            List<string> strings = new List<string>();

            foreach (string value in enumerable)
            {
                strings.Add(Encode(value, shift));
            }
        
            return strings;
        }

        /// <summary>
        /// Decodes an encoded string of CaesarCipher text to a normal string of latin alphabet text, shifting each letter by a specified key.
        /// </summary>
        /// <param name="s">The string to be decoded.</param>
        /// <param name="shift">The amount to shift letters by.</param>
        /// <returns>The decoded string.</returns>
        public string Decode(string s, int shift)
        {
            StringBuilder stringBuilder = new StringBuilder();
        
            for (int characterIndex = 0; characterIndex < s.Length; characterIndex++)
            {
                char character = s[characterIndex];

                if (character.ToString().Equals(character.ToString().ToUpper()))
                {
                    character = Convert.ToChar(CipherHelper.Alphabet[CipherHelper.OldPositionCalculator(CipherHelper.GetAlphabetPosition(character), shift)].ToString().ToUpper());
                }
                else if (character.ToString().Equals(character.ToString().ToLower()))
                {
                    character = CipherHelper.Alphabet[CipherHelper.OldPositionCalculator(CipherHelper.GetAlphabetPosition(character), shift)];
                }

                stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Decodes an IEnumerable of strings of CaesarCipher text to a normal string of latin alphabet text, shifting each letter by a specified key.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="shift">The amount to shift letters by.</param>
        /// <returns>The decoded strings as an IEnumerable.</returns>
        public IEnumerable<string> Decode(IEnumerable<string> enumerable, int shift)
        {
            List<string> strings = new List<string>();

            foreach (string value in enumerable)
            {
                strings.Add(Decode(value, shift));
            }

            return strings;
        }
    }
}