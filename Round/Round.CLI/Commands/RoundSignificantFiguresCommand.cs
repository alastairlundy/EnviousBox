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

using AlastairLundy.Extensions.Maths.SignificantFigures;

using Round.Cli.Helpers;
using Round.Cli.Localizations;
using Round.Cli.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Round.Cli.Commands;

internal class RoundSignificantFiguresCommand : Command<RoundSignificantFiguresCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.NumberToRound == null)
        {
            AnsiConsole.WriteException(new NullReferenceException(Resources.Error_Number_NoInput), ExceptionFormats.ShowLinks);
            return -1;
        }

        bool wasPrecisionProvided;
        decimal roundedValue = (decimal)settings.NumberToRound;

        if (settings.NumberOfSignificantFiguresToRoundTo == int.MinValue)
        {
            wasPrecisionProvided = false;
            roundedValue = decimal.Parse(roundedValue.ReturnToNSignificantFigures(2));
        }
        else
        {
            wasPrecisionProvided = true;
            roundedValue = decimal.Parse(roundedValue.ReturnToNSignificantFigures(settings.NumberOfSignificantFiguresToRoundTo));
        }


        if (!settings.PrettyMode)
        {
            AnsiConsole.WriteLine(roundedValue.ToString(CultureInfo.CurrentCulture));
            return 0;
        }
        else {

            Text providedValueLabel = new Text($"{Resources.Input_ProvidedValue}:", new Style(Color.IndianRed)).LeftJustified();
            Text providedValueActual = new Text(settings.NumberToRound.ToString()!, new Style(Color.DarkSeaGreen)).Centered();

            Text providedPrecisionLabel;

            if (wasPrecisionProvided)
            {
                providedPrecisionLabel = new Text($"{Resources.Input_Rounding_SignificantFigures}:", new Style(Color.IndianRed)).LeftJustified();
            }
            else
            {
                providedPrecisionLabel = new Text($"{Resources.Input_Rounding_SignificantFigures_NotProvided}:", new Style(Color.IndianRed)).LeftJustified();
            }

            Text providedPrecisionActual = new Text(settings.NumberOfSignificantFiguresToRoundTo.ToString(), new Style(Color.DarkSeaGreen)).Centered();

            Text resultLabel = new Text($"{Resources.Input_RoundedValue}:", new Style(Color.IndianRed)).LeftJustified();
            Text resultActual = new Text(roundedValue.ToString(CultureInfo.CurrentCulture), new Style(Color.Gold1)).Centered();


            AnsiConsole.Write(ConsoleGridHelper.CreateResultGrid(providedValueLabel, providedValueActual, providedPrecisionLabel, providedPrecisionActual, resultLabel, resultActual));
            return 0;
        }
    }

    internal class Settings: BaseSettings
    {
        [CommandArgument(0, "<number_to_round>")]
        public decimal? NumberToRound { get; init; }

        [CommandOption("-sf|--significant-figures")]
        [DefaultValue(int.MinValue)]
        public int NumberOfSignificantFiguresToRoundTo { get; init; }
    }
}
