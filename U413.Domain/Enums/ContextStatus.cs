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

namespace U413.Domain.Enums
{
    /// <summary>
    /// Represents the available options for a command context object.
    /// </summary>
    public enum ContextStatus
    {
        /// <summary>
        /// No context is currently active.
        /// </summary>
        Disabled,

        /// <summary>
        /// A context is active but existing commands take priority.
        /// </summary>
        Passive,

        /// <summary>
        /// A context is active and all data returned fromt he client (except for the cancel command)
        /// will be passed to the active command method.
        /// </summary>
        Forced,
    }
}
