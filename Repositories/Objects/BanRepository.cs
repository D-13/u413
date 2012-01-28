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
using U413.Domain.Repositories.Interfaces;
using U413.Domain.Entities;

namespace U413.Domain.Repositories.Objects
{
    /// <summary>
    /// A repository for persisting bans to the Entity Framework data context.
    /// </summary>
    public class BanRepository : IBanRepository
    {
        #region dependencies

       /// <summary>
        /// Every repository requires an instance of the Entity Framework data context.
        /// </summary>
        EntityContainer _entityContainer;

        public BanRepository(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        #endregion

        #region Interface Members

        /// <summary>
        /// Get a ban by the banned user.
        /// </summary>
        /// <param name="username">The banned user.</param>
        /// <returns>A ban entity.</returns>
        public Ban GetBanBy_User(string username)
        {
            var query = _entityContainer.Bans
                .SingleOrDefault(x => x.Username == username);
            return query;
        }

        /// <summary>
        /// Add a ban to the data context.
        /// </summary>
        /// <param name="ban">The ban to be added.</param>
        public void AddBan(Ban ban)
        {
            _entityContainer.Bans.Add(ban);
        }

        /// <summary>
        /// This method is not used. Please just call SaveChanges().
        /// </summary>
        /// <param name="message">The ban to be updated.</param>
        [Obsolete]
        public void UpdateBan(Ban ban)
        {
            // Do not throw a not implemented exception.
            // If this method is called, do nothing.
            //
            // This allows the code above this layer to use this method
            // in case a future data store needs it.
        }

        /// <summary>
        /// Deletes a ban from the data context.
        /// </summary>
        /// <param name="ban">The ban to be deleted.</param>
        public void DeleteBan(Ban ban)
        {
            _entityContainer.Bans.Remove(ban);
        }

        /// <summary>
        /// Persist changes to the data context.
        /// </summary>
        public void SaveChanges()
        {
            _entityContainer.SaveChanges();
        }

        #endregion

        #region Expressions

        #endregion
    }
}
