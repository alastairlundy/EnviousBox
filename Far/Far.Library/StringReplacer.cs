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

using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlastairLundy.Extensions.System;

using Far.Library.Abstractions;

namespace Far.Library;

public class StringReplacer : IStringReplacer
{
    /// <summary>
    /// Replaces exact matches of a specified char within a string with a specified replacement char.
    /// </summary>
    /// <param name="original">The string to be searched.</param>
    /// <param name="toBeReplaced">The char to be replaced.</param>
    /// <param name="replacementChar">The char to be used as the replacement.</param>
    /// <returns>the modified string if the string contained the char to be replaced; the original string otherwise.</returns>
    public string ReplaceCharacter(string original, char toBeReplaced, char replacementChar)
    {
        StringBuilder builder = new StringBuilder();
        
        for (int characters = 0; characters < original.Length; characters++)
        {
            if (original[characters].Equals(toBeReplaced))
            {
                builder.Append(replacementChar);
            }
            else
            {
                builder.Append(original[characters]);
            }
        }
            
        return builder.ToString();
    }
    
    /// <summary>
    /// Replaces a specified character in an IEnumerable of strings with a specified replacement character.
    /// </summary>
    /// <param name="enumerable">The IEnumerable of strings to be searched.</param>
    /// <param name="toBeReplaced">The char to be replaced.</param>
    /// <param name="replacementChar">The char to be used as the replacement.</param>
    /// <returns>the modified IEnumerable of strings if the char to be replaced was found; the unmodified original IEnumerable otherwise.</returns>
    public IEnumerable<string> ReplaceCharacter(IEnumerable<string> enumerable, char toBeReplaced, char replacementChar)
    {
        List<string> output = new();
        output.TrimExcess();
        
        string[] strings = enumerable.ToArray();

        foreach (string s in strings)
        {
            string tempStr = s;

            if (s.Contains(toBeReplaced))
            {
                tempStr = ReplaceCharacter(tempStr, toBeReplaced, replacementChar);
            }
            
            output.Add(tempStr);
        }

        return output;
    }

    /// <summary>
    /// Replaces exact matches of a specified string within a string with a specified replacement string.
    /// </summary>
    /// <param name="original">The string to be searched.</param>
    /// <param name="toBeReplaced">The string to be replaced.</param>
    /// <param name="replacementString">The replacement string.</param>
    /// <returns>the modified string if the string contained the string to be replaced; the original string otherwise.</returns>
    public string ReplaceExactMatch(string original, string toBeReplaced, string replacementString)
    {
        string output = original;
        
        if (original.Contains(toBeReplaced))
        {
            output = original.Replace(toBeReplaced, replacementString);
        }
        
        return output;
    }
    
    /// <summary>
    /// Replaces exact matches of a specified string in an IEnumerable of strings with a specified replacement string.
    /// </summary>
    /// <param name="enumerable">The IEnumerable of strings to be searched.</param>
    /// <param name="toBeReplaced">The string to be replaced.</param>
    /// <param name="replacementString">The replacement string.</param>
    /// <returns>the modified IEnumerable of strings if the string to be replaced was found; the unmodified original IEnumerable otherwise.</returns>
    public IEnumerable<string> ReplaceExactMatch(IEnumerable<string> enumerable, string toBeReplaced, string replacementString)
    {
        List<string> output = new();
        
        foreach (string str in enumerable)
        {
            string tempStr = str;
            
            if (str.Contains(toBeReplaced))
            {
                tempStr = ReplaceExactMatch(str, toBeReplaced, replacementString);
            }
            
            output.Add(tempStr);
        }

        return output;
    }

    /// <summary>
    /// Replaces partial matches of a specified string within a string with a specified replacement string.
    /// </summary>
    /// <param name="original">The string to be searched.</param>
    /// <param name="toBeReplaced">The string to be replaced.</param>
    /// <param name="replacementString">The replacement string.</param>
    /// <returns>the modified IEnumerable of strings if the string to be replaced was found; the unmodified original IEnumerable otherwise.</returns>
    public string ReplacePartialMatch(string original, string toBeReplaced, string replacementString)
    {
        string output = original;

        if (output.ToLower().Contains(toBeReplaced.ToLower()))
        {
            string tempOutput = output.ToLower().Replace(toBeReplaced.ToLower(), replacementString);

            for(int index = 0; index < tempOutput.Length; index++)
            {
                string currentTemp = tempOutput[index].ToString().ToLower();
                string current = output[index].ToString().ToLower();
                
                if (currentTemp.Equals(current))
                {
                    if (output[index].IsUpperCaseLetter())
                    {
                        tempOutput = tempOutput.Insert(index, current.ToUpper());
                        tempOutput = tempOutput.Remove(index + 1, 1);
                    }
                }
            }
            
            output = tempOutput;
        }
        
        return output;
    }

    /// <summary>
    /// Replaces partial matches of a specified string in an IEnumerable of strings with a specified replacement string.
    /// </summary>
    /// <param name="enumerable">The IEnumerable of strings to be searched.</param>
    /// <param name="toBeReplaced">The string to be replaced.</param>
    /// <param name="replacementString">The replacement string.</param>
    /// <returns>the modified IEnumerable of strings if the string to be replaced was found; the unmodified original IEnumerable otherwise.</returns>
    public IEnumerable<string> ReplacePartialMatch(IEnumerable<string> enumerable, string toBeReplaced, string replacementString)
    {
        List<string> output = new();

        foreach (string str in enumerable)
        {
            string tempStr = str;
            
            tempStr = ReplacePartialMatch(tempStr, toBeReplaced, replacementString);
            
            output.Add(tempStr);
        }

        return output;
    }
}