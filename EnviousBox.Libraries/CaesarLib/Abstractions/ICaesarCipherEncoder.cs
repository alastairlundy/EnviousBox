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

using Microsoft.Extensions.Primitives;

namespace AlastairLundy.CaesarLib.Abstractions
{
    public interface ICaesarCipherEncoder
    {
        char EncodeCharacter(char character, int shift);
        
        string EncodeString(string stringToEncode, int shift);
        
        StringSegment EncodeStringSegment(StringSegment stringToEncode, int shift);
    }
}