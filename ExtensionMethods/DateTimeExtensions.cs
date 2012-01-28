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
    /// Extensions to DateTime objects.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Extension to convert a DateTime to the specified timezone.
        /// </summary>
        /// <param name="dateTime">The DateTime to be converted.</param>
        /// <param name="timeZone">The time zone to convert the date/time to.</param>
        /// <returns>A modified DateTime object.</returns>
        public static DateTime ConvertToTimezone(this DateTime dateTime, string timeZone)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timeZone));
        }

        /// <summary>
        /// Total time passed since specified date.
        /// </summary>
        /// <param name="dateTime">Date to subtract from current date.</param>
        /// <returns>string</returns>
        public static string TimePassed(this DateTime dateTime)
        {
            TimeSpan timePassed = DateTime.UtcNow.Subtract(dateTime);

            string time;
            if (timePassed.TotalDays >= 365)
            {
                int years = (int)(timePassed.TotalDays / 365);
                time = Pluralize(years, "year");
            }
            else if (timePassed.TotalDays >= 30)
            {
                int months = (int)(timePassed.TotalDays / 30);
                time = Pluralize(months, "month");
            }
            else if (timePassed.TotalDays >= 7)
            {
                int weeks = (int)(timePassed.TotalDays / 7);
                time = Pluralize(weeks, "week");
            }
            else if (timePassed.TotalHours >= 24)
            {
                int days = (int)timePassed.TotalDays;
                time = Pluralize(days, "day");
            }
            else if (timePassed.TotalMinutes >= 60)
            {
                int hours = (int)timePassed.TotalHours;
                time = Pluralize(hours, "hour");
            }
            else if (timePassed.TotalMinutes >= 1)
            {
                int minutes = (int)timePassed.TotalMinutes;
                time = Pluralize(minutes, "minute");
            }
            else
            {
                int seconds = (int)timePassed.TotalSeconds;
                time = Pluralize(seconds, "second");
            }

            return string.Format("{0} ago", time);
        }

        /// <summary>
        /// Total time passed since specified date.
        /// </summary>
        /// <param name="dateTime">Date to subtract from current date.</param>
        /// <returns>string</returns>
        public static string TimeUntil(this DateTime dateTime)
        {
            TimeSpan timeUntil = -DateTime.UtcNow.Subtract(dateTime);

            string time;
            if (timeUntil.TotalDays >= 365)
            {
                int years = (int)(timeUntil.TotalDays / 365);
                time = Pluralize(years, "year");
            }
            else if (timeUntil.TotalDays >= 30)
            {
                int months = (int)(timeUntil.TotalDays / 30);
                time = Pluralize(months, "month");
            }
            else if (timeUntil.TotalDays >= 7)
            {
                int weeks = (int)(timeUntil.TotalDays / 7);
                time = Pluralize(weeks, "week");
            }
            else if (timeUntil.TotalHours >= 24)
            {
                int days = (int)timeUntil.TotalDays;
                time = Pluralize(days, "day");
            }
            else if (timeUntil.TotalMinutes >= 60)
            {
                int hours = (int)timeUntil.TotalHours;
                time = Pluralize(hours, "hour");
            }
            else if (timeUntil.TotalMinutes >= 1)
            {
                int minutes = (int)timeUntil.TotalMinutes;
                time = Pluralize(minutes, "minute");
            }
            else
            {
                int seconds = (int)timeUntil.TotalSeconds;
                time = Pluralize(seconds, "second");
            }

            return string.Format("{0} from now", time);
        }

        #region Private Methods

        /// <summary>
        /// Pluralizes the specified noun, based on the specified count.
        /// </summary>
        /// <param name="num">The count.</param>
        /// <param name="noun">Noun, pluralizable by adding 's' to the end.</param>
        /// <returns>string</returns>
        private static string Pluralize(int count, string noun)
        {
            if (count == 1)
                return string.Format("{0} {1}", count, noun);
            else
                return string.Format("{0} {1}s", count, noun);
        }

        #endregion
    }
}
