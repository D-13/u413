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
    /// A repository for storing application variables.
    /// </summary>
    public interface IVariableRepository
    {
        /// <summary>
        /// Adds or edits a variable in the repository.
        /// </summary>
        /// <param name="variable">The variable to be added.</param>
        void ModifyVariable(Variable variable);

        /// <summary>
        /// Gets a variable from the variable repository.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        Variable GetVariable(string name);
    }
}