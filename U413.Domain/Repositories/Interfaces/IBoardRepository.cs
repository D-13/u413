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
    /// A repository for storing boards.
    /// </summary>
    public interface IBoardRepository
    {
        /// <summary>
        /// Adds a board to the repository.
        /// </summary>
        /// <param name="board">The board to be added.</param>
        void AddBoard(Board board);

        /// <summary>
        /// Updates an existing board in the repository.
        /// </summary>
        /// <param name="board">The board to be updated.</param>
        void UpdateBoard (Board board);

        /// <summary>
        /// Deletes a board from the repository.
        /// </summary>
        /// <param name="board">The board to be deleted.</param>
        void DeleteBoard(Board board);

        /// <summary>
        /// Retrieve a board by its unique ID.
        /// </summary>
        /// <param name="boardID">The unique ID of the desired board.</param>
        /// <returns>A board entity.</returns>
        Board GetBoard(short boardID);

        /// <summary>
        /// Retrieve an enumerable list of all boards.
        /// </summary>
        /// <param name="loadModeratorBoards">True if moderator-only boards should be included.</param>
        /// <returns>An enumerable list of boards.</returns>
        IEnumerable<Board> GetBoards(bool loadModeratorBoards);
    }
}