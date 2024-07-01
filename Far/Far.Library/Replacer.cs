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
    /// <param name="filePath"></param>
    /// <param name="existingChar"></param>
    /// <param name="replacementChar"></param>
    /// <returns></returns>
    public static string[] ReplaceCharacter(string filePath, char existingChar, char replacementChar)
    {
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] words = line.Split(' ');

            for(int index = 0; index < words.Length; index++)
            {
                for(int characters = 0; characters < words[index].Length; characters++)
                {
                    if (words[index][characters].Equals(existingChar))
                    {
                        words[index] = words[index].Replace(existingChar, replacementChar);
                    }   
                }
            }
        }

        return lines;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="existingString"></param>
    /// <param name="replacementString"></param>
    /// <returns></returns>
    public static string[] ReplaceExactMatch(string filePath, string existingString, string replacementString)
    {
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] words = line.Split(' ');

            for (int index = 0; index < words.Length; index++)
            {
                if (words[index].Equals(existingString)){
                    words[index] = replacementString;
                }
            }
        }

        return lines;
    }

    public static string[] ReplacePartialMatch(string filePath, string existingString, string replacementString)
    {
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] words = line.Split(' ');

            for (int index = 0; index < words.Length; index++)
            {
                if (words[index].Contains(existingString))
                {
                    words[index] = words[index].Replace(existingString, replacementString);
                }
            }
        }

        return lines;
    }
}