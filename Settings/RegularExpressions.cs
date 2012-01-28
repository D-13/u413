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

namespace U413.Domain.Settings
{
    /// <summary>
    /// Static class containing common regular expression strings.
    /// </summary>
    public static class RegularExpressions
    {
        /// <summary>
        /// Expression to find all root-level BBCode tags. Use this expression recursively to obtain nested tags.
        /// </summary>
        public static string BBCodeTags
        {
            get
            {
                return @"
                        (?>
                        \[ (?<tag>[^][/=\s]+) \s*
                        (?: = \s* (?<val>[^][]*) \s*)?
                        \]
                        )
                          (?<content>
                            (?>
                               \[(?:unsuccessful)\]  # self closing
                               |
                               \[(?<innertag>[^][/=\s]+)[^][]*]
                               |
                               \[/(?<-innertag>\k<innertag>)]
                               |
                               .
                            )*
                            (?(innertag)(?!))
                          )
                        \[/\k<tag>\]
                        ";
            }
        }
    }
}
