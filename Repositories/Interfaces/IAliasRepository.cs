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
    /// A repository for storing command aliases.
    /// </summary>
    public interface IAliasRepository
    {
        /// <summary>
        /// Adds an alias to the repository.
        /// </summary>
        /// <param name="alias">The alias to be added.</param>
        void AddAlias(Alias alias);

        /// <summary>
        /// Deletes an alias from the repository.
        /// </summary>
        /// <param name="alias">The alias to be deleted.</param>
        void DeleteAlias(Alias alias);

        /// <summary>
        /// Obtains an alias by username and shortcut.
        /// </summary>
        /// <param name="username">The name of the user specifying the alias.</param>
        /// <param name="shortcut">The shortcut defined by the user.</param>
        /// <returns>A command alias.</returns>
        Alias GetAlias(string username, string shortcut);

        /// <summary>
        /// Get all aliases by username.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns>An enumerable list of aliases.</returns>
        IEnumerable<Alias> GetAliases(string username);
    }
}