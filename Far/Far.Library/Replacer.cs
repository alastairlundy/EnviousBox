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

namespace Far.Library;

public static class Replacer
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="enumerable"></param>
    /// <param name="existingChar">The char to be replaced.</param>
    /// <param name="replacementChar">The char to use as a replacement.</param>
    /// <returns></returns>
    public static IEnumerable<string> ReplaceCharacter(IEnumerable<string> enumerable, char existingChar, char replacementChar)
    {
        List<string> output = new();
        output.TrimExcess();
        
        string[] strings = enumerable.ToArray();

        foreach (string s in strings)
        {
            string[] words = s.Split(' ');

            for (int index = 0; index < words.Length; index++)
            {
                for (int characters = 0; characters < words[index].Length; characters++)
                {
                    if (words[index][characters].Equals(existingChar))
                    {
                        words[index] = words[index].Replace(existingChar, replacementChar);
                    }
                }
            }

            string? newWords = words.ToString();
            
            if (newWords is not null)
            {
                output.Add(newWords);
            }
             
        }

        return output;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enumerable"></param>
    /// <param name="existingString"></param>
    /// <param name="replacementString"></param>
    /// <returns></returns>
    public static IEnumerable<string> ReplaceExactMatch(IEnumerable<string> enumerable, string existingString, string replacementString)
    {
        List<string> output = new();
        
        foreach (string str in enumerable)
        {
            string[] lines = str.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                string[] words = line.Split(' ');

                for (int index = 0; index < words.Length; index++)
                {
                    if (words[index].Equals(existingString))
                    {
                        words[index] = replacementString;
                    }
                }
            }

            if (lines.ToString() is not null)
            {
                output.Add(lines.ToString()!);
            }
        }

        return output;
    }

    public static IEnumerable<string> ReplacePartialMatch(IEnumerable<string> enumerable, string existingString, string replacementString)
    {
        List<string> output = new();

        foreach (string str in enumerable)
        {
            string[] lines = str.Split(Environment.NewLine);
            
            foreach (string line in lines)
            {
                string[] words = line.Split(' ');

                for (int index = 0; index < words.Length; index++)
                {
                    if (words[index].Equals(existingString))
                    {
                        words[index] = replacementString;
                    }
                }
            }

            if (lines.ToString() is not null)
            {
                output.Add(lines.ToString());
            }
        }

        return output;
    }
}