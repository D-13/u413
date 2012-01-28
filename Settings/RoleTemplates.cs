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
    /// Pre-defined role templates for use inside command classes.
    /// </summary>
    public static class RoleTemplates
    {
        /// <summary>
        /// Returns only the visitor role.
        /// </summary>
        public static string[] Visitor
        {
            get { return new string[] { "Visitor" }; }
        }

        /// <summary>
        /// Returns only the user role.
        /// </summary>
        public static string[] OnlyUsers
        {
            get { return new string[] { "User" }; }
        }

        /// <summary>
        /// Returns the moderator and user roles.
        /// </summary>
        public static string[] ModsAndUsers
        {
            get { return new string[] { "User", "Moderator" }; }
        }

        /// <summary>
        /// Returns all roles for logged in users.
        /// </summary>
        public static string[] AllLoggedIn
        {
            get { return new string[] { "User", "Moderator", "Administrator" }; }
        }

        /// <summary>
        /// Returns all roles.
        /// </summary>
        public static string[] Everyone
        {
            get { return new string[] { "Visitor", "User", "Moderator", "Administrator" }; }
        }
    }
}
