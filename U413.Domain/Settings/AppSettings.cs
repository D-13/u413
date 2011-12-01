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
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace U413.Domain.Settings
{
    /// <summary>
    /// Various static application settings.
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// The database connection string.
        /// </summary>
        /// <remarks>
        /// This connection string is checked into source control and is publicly available.
        /// When checking out the source code you are welcome to use this database.
        /// Please do not abuse this priviledge and ruin it for everybody by forcing
        /// me to take the development database offline due to abuse.
        /// </remarks>
        private static string _connectionString = "metadata=res://*/Entities.Entities.csdl|res://*/Entities.Entities.ssdl|res://*/Entities.Entities.msl;provider=System.Data.SqlClient;provider connection string=\"data source=chevex1.arvixevps.com;initial catalog=devU413;persist security info=True;user id=devU413;password=PIrates;multipleactiveresultsets=True;App=EntityFramework\"";
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// The logo to display on application initialization.
        /// </summary>
        public static string Logo
        {
            get
            {
                return @"
  __  __  __ __       _     __
 /\ \/\ \/\ \\ \    /' \  /'__`\
 \ \ \ \ \ \ \\ \  /\_, \/\_\L\ \
  \ \ \ \ \ \ \\ \_\/_/\ \/_/_\_<_
   \ \ \_\ \ \__ ,__\ \ \ \/\ \L\ \
    \ \_____\/_/\_\_/  \ \_\ \____/
     \/_____/  \/_/     \/_/\/___/";
            }
        }

        private static int _maxLineLength = 95;
        public static int MaxLineLength
        {
            get { return _maxLineLength; }
            set { _maxLineLength = value; }
        }

        public static int DividerLength
        {
            get { return _maxLineLength / 2; }
        }

        /// <summary>
        /// The number of topics to display per page.
        /// </summary>
        public static int TopicsPerPage
        {
            get { return 15; }
        }

        /// <summary>
        /// The number of replies to display per page.
        /// </summary>
        public static int RepliesPerPage
        {
            get { return 15; }
        }

        public static int MessagesPerPage
        {
            get { return 15; }
        }
    }
}
