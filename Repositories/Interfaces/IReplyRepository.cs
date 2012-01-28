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
    /// Repository for storing topic replies.
    /// </summary>
    public interface IReplyRepository
    {
        /// <summary>
        /// Adds a reply to the repository.
        /// </summary>
        /// <param name="reply">The reply to be added.</param>
        void AddReply(Reply reply);

        /// <summary>
        /// Updates an existing reply in the repository.
        /// </summary>
        /// <param name="reply">The reply to be added.</param>
        void UpdateReply (Reply reply);

        /// <summary>
        /// Deletes a reply from the repository.
        /// </summary>
        /// <param name="reply">The reply to be deleted.</param>
        void DeleteReply(Reply reply);

        /// <summary>
        /// Get a reply by its unique ID.
        /// </summary>
        /// <param name="replyID">The unique ID of the reply.</param>
        /// <returns>A reply entity.</returns>
        Reply GetReply(long replyID);

        /// <summary>
        /// Get all replies for a single topic.
        /// </summary>
        /// <param name="topicID">The unique ID of the topic.</param>
        /// <param name="isModerator">True if moderator-only replies should be returned.</param>
        /// <returns>An enumerable list of replies.</returns>
        CollectionPage<Reply> GetReplies(long topicID, int page, int itemsPerPage, bool isModerator);
    }
}