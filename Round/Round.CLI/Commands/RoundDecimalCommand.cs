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
using System.ComponentModel;
using System.Globalization;

using Round.Cli.Helpers;
using Round.Cli.Localizations;
using Round.Cli.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Round.Cli.Commands;

internal class RoundDecimalCommand : Command<RoundDecimalCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        bool wasPrecisionProvided;
        decimal roundedValue;

        if (settings.NumberToRound == null)
        {
            AnsiConsole.WriteException(new NullReferenceException(Resources.Error_Number_NoInput), ExceptionFormats.ShowLinks);
            return -1;
        }

        if (settings.NumberOfDecimalPlacesToUse == int.MinValue)
        {
            wasPrecisionProvided = false;
            roundedValue = decimal.Round((decimal)settings.NumberToRound!, 2);
        }
        else
        {
            wasPrecisionProvided = true;
            roundedValue = decimal.Round((decimal)settings.NumberToRound!, settings.NumberOfDecimalPlacesToUse);
        }

        if (!settings.PrettyMode)
        {
            AnsiConsole.WriteLine(roundedValue.ToString(CultureInfo.CurrentCulture));
            return 0;
        }
        else
        {
            Text providedValueLabel = new Text($"{Resources.Input_ProvidedValue}:", new Style(Color.IndianRed)).LeftJustified();
            Text providedValueActual = new Text(settings.NumberToRound.ToString()!, new Style(Color.DarkSeaGreen)).Centered();

            Text providedPrecisionLabel;

            if (wasPrecisionProvided)
            {
                providedPrecisionLabel = new Text($"{Resources.Input_Rounding_DecimalPlaces}:", new Style(Color.IndianRed)).LeftJustified();
            }
            else
            {
                providedPrecisionLabel = new Text($"{Resources.Input_Rounding_DecimalPlaces_NotProvided}:", new Style(Color.IndianRed)).LeftJustified();
            }

            Text providedPrecisionActual = new Text(settings.NumberOfDecimalPlacesToUse.ToString(), new Style(Color.DarkSeaGreen)).Centered();

            Text resultLabel = new Text($"{Resources.Input_RoundedValue}:", new Style(Color.IndianRed)).LeftJustified();
            Text resultActual = new Text(roundedValue.ToString(CultureInfo.CurrentCulture), new Style(Color.Gold1)).Centered();

            AnsiConsole.Write(ConsoleGridHelper.CreateResultGrid(providedValueLabel, 
                providedValueActual, providedPrecisionLabel, providedPrecisionActual, resultLabel, resultActual));
            return 0;
        }
    }

    internal class Settings : BaseSettings
    {
        [CommandArgument(0, "<number_to_round>")]
        public decimal? NumberToRound { get; init; }

        [CommandOption("-dp|--decimal-places")]
        [DefaultValue(int.MinValue)]
        public int NumberOfDecimalPlacesToUse { get; init; }
    }
}