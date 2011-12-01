//   **************************************************************************
//   *                                                                        *
//   *  This program is free software: you can redistribute it and/or modify  *
//   *  it under the terms of the GNU General Public License as published by  *
//   *  the Free Software Foundation, either version 3 of the License, or     *
//   *  (at your option) any later version.                                   *
//   *                                                                        *
//   *  This program is distributed in the hope that it will be useful,       *
//   *  but WITHOUT ANY WARRANTY; without even the implied warranty of        *
//   *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
//   *  GNU General Public License for more details.                          *
//   *                                                                        *
//   *  You should have received a copy of the GNU General Public License     *
//   *  along with this program.  If not, see <http://www.gnu.org/licenses/>. *
//   *                                                                        *
//   **************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace U413.Domain.ExtensionMethods
{
    /// <summary>
    /// Extentions to string types.
    /// </summary>
    public static class StringExtensions
    {
        public static string WrapOnSpace(this string text, int maxLineLength)
        {
            Func<string, string> wrapLine = line =>
            {
                var numNewLines = line.Length / maxLineLength;
                var newLine = line;
                for (int count = 0; count < numNewLines; count++)
                {
                    var subString = line.Substring(count * maxLineLength, maxLineLength);
                    if (subString.Contains(' '))
                    {
                        var index = subString.LastIndexOf(' ');
                        newLine = newLine
                            .Insert(index + (maxLineLength * count), "\n")
                            .Remove((index + (maxLineLength * count)) + 1, 1);
                    }
                }
                return newLine;
            };

            var existingLines = text.Split('\n');
            var wrappedText = new StringBuilder();
            foreach (var line in existingLines)
            {
                wrappedText.Append(wrapLine(line));
                if (existingLines.Length > 1)
                    wrappedText.Append('\n');
            }
            return wrappedText.ToString();
        }

        /// <summary>
        /// Checks if a string array is null or contains no elements.
        /// </summary>
        /// <param name="arg">The value to be avaluated.</param>
        /// <returns>True if the array is null or contains no elements.</returns>
        public static bool IsNullOrEmpty(this string[] arg)
        {
            return arg == null || arg.Length == 0;
        }

        public static bool IsNullOrEmpty(this string arg)
        {
            return string.IsNullOrEmpty(arg);
        }

        public static bool IsNullOrWhiteSpace(this string arg)
        {
            return string.IsNullOrWhiteSpace(arg);
        }

        /// <summary>
        /// Check if the value of the string is a 16-bit integer.
        /// </summary>
        /// <param name="arg">The value to be evaluated.</param>
        /// <returns>True if the value is a 16-bit integer.</returns>
        public static bool IsShort(this string arg)
        {
            try
            {
                Convert.ToInt16(arg);
                return arg != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the value of the string is a 32-bit integer.
        /// </summary>
        /// <param name="arg">The value to be evaluated.</param>
        /// <returns>True if the value is a 32-bit integer.</returns>
        public static bool IsInt(this string arg)
        {
            try
            {
                Convert.ToInt32(arg);
                return arg != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the value of the string is a 64-bit integer.
        /// </summary>
        /// <param name="arg">The value to be evaluated.</param>
        /// <returns>True if the value is a 64-bit integer.</returns>
        public static bool IsLong(this string arg)
        {
            try
            {
                Convert.ToInt64(arg);
                return arg != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Convert the string to a 16-bit integer.
        /// </summary>
        /// <param name="arg">The value to be converted.</param>
        /// <returns>A 16-bit integer.</returns>
        public static short ToShort(this string arg)
        {
            return Convert.ToInt16(arg);
        }

        /// <summary>
        /// Convert the string to a 32-bit integer.
        /// </summary>
        /// <param name="arg">The value to be converted.</param>
        /// <returns>A 32-bit integer.</returns>
        public static int ToInt(this string arg)
        {
            return Convert.ToInt32(arg);
        }

        /// <summary>
        /// Convert the string to a 64-bit integer.
        /// </summary>
        /// <param name="arg">The value to be converted.</param>
        /// <returns>A 64-bit integer.</returns>
        public static long ToLong(this string arg)
        {
            return Convert.ToInt64(arg);
        }

        /// <summary>
        /// Short-hand method for comparing two strings while ignoring case.
        /// </summary>
        /// <param name="arg">The string initiating the comparison.</param>
        /// <param name="valueToCompare">The string to compare.</param>
        /// <returns>True if the two strings are equal.</returns>
        public static bool Is(this string arg, string valueToCompare)
        {
            return arg.Equals(valueToCompare, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
