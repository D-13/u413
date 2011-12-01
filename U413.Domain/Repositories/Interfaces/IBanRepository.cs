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

namespace U413.Domain.Repositories.Interfaces
{
    /// <summary>
    /// A repository to store bans.
    /// </summary>
    public interface IBanRepository
    {
        /// <summary>
        /// Gets a ban by the banned user.
        /// </summary>
        /// <param name="username">The user who has been banned.</param>
        /// <returns>A ban entity.</returns>
        Ban GetBanBy_User(string username);

        /// <summary>
        /// Add a ban to the repository.
        /// </summary>
        /// <param name="ban">The ban to be added.</param>
        void AddBan(Ban ban);

        /// <summary>
        /// Updates an existing ban in the repository.
        /// </summary>
        /// <param name="ban">The ban to be updated.</param>
        void UpdateBan(Ban ban);

        /// <summary>
        /// Delete a ban from the repository.
        /// </summary>
        /// <param name="ban">The ban to be deleted.</param>
        void DeleteBan(Ban ban);

        /// <summary>
        /// Instruct the repository to persist changes to the underlying data store.
        /// </summary>
        void SaveChanges();
    }
}