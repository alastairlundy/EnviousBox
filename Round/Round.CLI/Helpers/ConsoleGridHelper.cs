/*
    EnviousBox - Round
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

using Spectre.Console;


namespace Round.Cli.Helpers;

internal class ConsoleGridHelper
{

    internal static Grid CreateResultGrid(Text providedValueLabel, Text providedValueActual, Text providedPrecisionLabel, Text providedPrecisionActual, Text resultLabel, Text resultActual)
    {
        Grid grid = new();

        grid.AddRow(
    new Padder[]
    {
                    new Padder(providedValueLabel).PadBottom(10),
                    new Padder(providedValueActual).PadBottom(10)
    });

        grid.AddRow(
            new Padder[] {
                    new Padder(providedPrecisionLabel).PadBottom(10),
                    new Padder(providedPrecisionActual).PadBottom(10)
        });
        grid.AddRow(
            new Padder[]
            {
                    new Padder(resultLabel).PadBottom(10),
                     new Padder(resultActual).PadBottom(10)
            });

        return grid;
    }
}
