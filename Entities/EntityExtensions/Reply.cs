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
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace U413.Domain.Entities
{
    /// <summary>
    /// Extension methods for the Reply entity.
    /// </summary>
    public partial class Reply
    {
        /// <summary>
        /// Checks if the reply, the topic, or the board is moderator only.
        /// </summary>
        /// <returns></returns>
        public bool IsModsOnly()
        {
            return (this.ModsOnly || this.Topic.ModsOnly || this.Topic.Board.ModsOnly);
        }
    }
}
