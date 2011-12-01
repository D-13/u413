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
using U413.Domain;
using U413.Domain.Repositories.Interfaces;
using U413.Domain.Entities;

namespace U413.Domain.Repositories.Objects
{
    /// <summary>
    /// Repository for persisting aliases to the Entity Framework data context.
    /// </summary>
    public class AliasRepository : IAliasRepository
    {
        /// <summary>
        /// Every repository requires an instance of the Entity Framework data context.
        /// </summary>
        EntityContainer _entityContainer;

        public AliasRepository(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        /// <summary>
        /// Adds an alias to the data context.
        /// </summary>
        /// <param name="alias">The alias to be added.</param>
        public void AddAlias(Alias alias)
        {
            _entityContainer.Aliases.Add(alias);
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Deletes an alias from the data context.
        /// </summary>
        /// <param name="alias">The alias to be deleted.</param>
        public void DeleteAlias(Alias alias)
        {
            _entityContainer.Aliases.Remove(alias);
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Retrieves an alias from the data context by username and shortcut.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="shortcut">The shortcut name.</param>
        /// <returns>An alias entity.</returns>
        public Alias GetAlias(string username, string shortcut)
        {
            var query = _entityContainer.Aliases
                .Where(x => x.Username.ToLower() == username.ToLower())
                .Where(x => x.Shortcut.ToLower() == shortcut.ToLower())
                .FirstOrDefault();
            return query;
        }

        /// <summary>
        /// Retrieves all aliases by username.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns>An enumerable list of aliases.</returns>
        public IEnumerable<Alias> GetAliases(string username)
        {
            var query = _entityContainer.Aliases
                .Where(x => x.Username.ToLower() == username.ToLower());
            return query;
        }
    }
}