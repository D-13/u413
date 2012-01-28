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
    /// A repository to store messages.
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Gets a message by its unique ID.
        /// </summary>
        /// <param name="messageId">The unique ID of the message.</param>
        /// <returns>A message entity.</returns>
        Message GetMessage(long messageId);

        /// <summary>
        /// Get received messages for user.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="page">The page number.</param>
        /// <param name="itemsPerPage">The number of items to display per page.</param>
        /// <returns>An enumerable list of messages.</returns>
        CollectionPage<Message> GetMessages(string username, int page, int itemsPerPage, bool sent);

        int UnreadMessages(string username);

        /// <summary>
        /// Retrieves a list of all messages from the data context for user.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="sent">True if retrieving sent messages.</param>
        /// <returns>An enumerable list of messages.</returns>
        IEnumerable<Message> GetAllMessages(string username, bool sent);

        /// <summary>
        /// Add a message to the repository.
        /// </summary>
        /// <param name="message">The message to be added.</param>
        void AddMessage(Message message);

        /// <summary>
        /// Updates an existing message in the repository.
        /// </summary>
        /// <param name="message">The message to be updated.</param>
        void UpdateMessage(Message message);

        /// <summary>
        /// Delete a message from the repository.
        /// </summary>
        /// <param name="message">The message to be deleted.</param>
        void DeleteMessage(Message message);
    }
}