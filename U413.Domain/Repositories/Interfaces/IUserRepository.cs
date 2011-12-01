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
using System.Web;
using U413.Domain.Entities;
using U413.Domain.Objects;

namespace U413.Domain.Repositories.Interfaces
{
    /// <summary>
    /// Repository for storing users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Add user to the repository.
        /// </summary>
        /// <param name="user">The user to be added.</param>
        void AddUser(User user);

        /// <summary>
        /// Update an existing user in the repository.
        /// </summary>
        /// <param name="user">The user to be updated.</param>
        void UpdateUser (User user);

        /// <summary>
        /// Delete a user from the repository.
        /// </summary>
        /// <param name="user">The user to be deleted.</param>
        void DeleteUser(User user);

        /// <summary>
        /// Get a user by the username.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns>A user entity.</returns>
        User GetUser(string username);

        /// <summary>
        /// Check if the username exists.
        /// </summary>
        /// <param name="username">The desired username.</param>
        /// <returns>True if the username exists.</returns>
        bool CheckUserExists(string username);

        /// <summary>
        /// Get the stored version of a username.
        /// Note: Preserves casing.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns>A correctly cased username.</returns>
        string GetStoredUsername(string username);

        /// <summary>
        /// Get all users active within the last ten minutes.
        /// </summary>
        /// <returns>An enumerable list of users.</returns>
        IEnumerable<User> GetLoggedInUsers();

        IEnumerable<User> GetModeratorsAndAdministrators();

        /// <summary>
        /// Associate a role with the user.
        /// </summary>
        /// <param name="roleName">The name of the role to associate.</param>
        void AddRoleToUser(User user, string roleName);

        UserStats GetUserStatistics();

        void IgnoreUser(string initiatingUsername, string ignoredUsername);

        void UnignoreUser(string initiatingUsername, string ignoredUsername);

        void UnbanUser(string username);

        IEnumerable<UserActivityLogItem> GetOffenseHistory(string username);
    }
}