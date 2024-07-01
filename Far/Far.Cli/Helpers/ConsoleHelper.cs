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

namespace Far.Cli.Helpers;

    internal class ConsoleHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static void PrintLicenseToConsole()
        {
            string[] licenseStrings =
                File.ReadAllLines($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}LICENSE.txt");

            foreach (string str in licenseStrings)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();
        }
    }