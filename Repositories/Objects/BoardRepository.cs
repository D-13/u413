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
    /// A repository for persisting boards to the Entity Framework data context.
    /// </summary>
    public class BoardRepository : IBoardRepository
    {
        /// <summary>
        /// Every repository requires an instance of the Entity Framework data context.
        /// </summary>
        EntityContainer _entityContainer;

        public BoardRepository(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        /// <summary>
        /// Adds a board to the data context.
        /// </summary>
        /// <param name="board">The board to be added.</param>
        public void AddBoard(Board board)
        {
            _entityContainer.Boards.Add(board);
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// UPdates an existing board in the data context.
        /// </summary>
        /// <param name="board">The board to be updated.</param>
        public void UpdateBoard(Board board)
        {
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Deletes a board from the data context.
        /// </summary>
        /// <param name="board">The board to be deleted.</param>
        public void DeleteBoard(Board board)
        {
            foreach (var topic in board.Topics.ToList())
            {
                topic.Replies.ToList().ForEach(x => _entityContainer.Replies.Remove(x));
                _entityContainer.Topics.Remove(topic);
            }
            _entityContainer.Boards.Remove(board);
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Retrive a board from the data context by its unique ID.
        /// </summary>
        /// <param name="boardID">The unique ID of the board.</param>
        /// <returns>A board entity.</returns>
        public Board GetBoard(short boardID)
        {
            var query = _entityContainer.Boards.Where(x => x.BoardID == boardID);
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Get all available discussion boards from the data context.
        /// </summary>
        /// <param name="isModerator">True if moderator-only boards should be included.</param>
        /// <returns>An enumerable list of boards.</returns>
        public IEnumerable<Board> GetBoards(bool isModerator)
        {
            var query = _entityContainer.Boards.AsEnumerable();
            if (!isModerator)
                query = query
                    .Where(x => !x.ModsOnly)
                    .Where(x => !x.Hidden);
            return query;
        }
    }
}