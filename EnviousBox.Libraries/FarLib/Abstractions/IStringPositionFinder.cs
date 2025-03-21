﻿/*
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

using System.Collections.Generic;
using AlastairLundy.FarLib.Models;

namespace AlastairLundy.FarLib.Abstractions;

public interface IStringPositionFinder
{
    public IEnumerable<CharPosition> GetCharPositions(string toBeSearched, char expected, bool ignoreCase);
    public IEnumerable<CharPosition> GetCharPositions(IEnumerable<string> strings, char expected, bool ignoreCase);

    public IEnumerable<StringPosition> GetStringPositions(string stringToBeSearched, string expected, bool ignoreCase);

    public IEnumerable<StringPosition> GetStringPositions(IEnumerable<string> strings, string expected, bool ignoreCase);
    
}