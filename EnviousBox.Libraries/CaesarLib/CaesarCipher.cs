/*
    EnviousBox - Caesar Library
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
using System.Text;

using AlastairLundy.CaesarLib.Helpers;
// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.CaesarLib
{
    /// <summary>
    /// 
    /// </summary>
    public class CaesarCipher
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        public string Encode(string value, int shift)
        {
            StringBuilder stringBuilder = new StringBuilder();
        
            for (int characterIndex = 0; characterIndex < value.Length; characterIndex++)
            {
                char character = value[characterIndex];

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
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        public IEnumerable<string> Encode(IEnumerable<string> values, int shift)
        {
            List<string> strings = new List<string>();

            foreach (string value in values)
            {
                strings.Add(Encode(value, shift));
            }
        
            return strings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        public string Decode(string value, int shift)
        {
            StringBuilder stringBuilder = new StringBuilder();
        
            for (int characterIndex = 0; characterIndex < value.Length; characterIndex++)
            {
                char character = value[characterIndex];

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
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        public IEnumerable<string> Decode(IEnumerable<string> values, int shift)
        {
            List<string> strings = new List<string>();

            foreach (string value in values)
            {
                strings.Add(Decode(value, shift));
            }

            return strings;
        }
    }
}