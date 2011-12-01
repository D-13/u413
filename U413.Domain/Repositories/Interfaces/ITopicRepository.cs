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
    /// Repository for storing topics.
    /// </summary>
    public interface ITopicRepository
    {
        /// <summary>
        /// Adds a topic to the repository.
        /// </summary>
        /// <param name="topic">The topic to be added.</param>
        void AddTopic(Topic topic);

        /// <summary>
        /// Updates an existing topic in the repository.
        /// </summary>
        /// <param name="topic">The topic to be updated.</param>
        void UpdateTopic (Topic topic);

        /// <summary>
        /// Delete a topic from the repository.
        /// </summary>
        /// <param name="topic">The topic to be deleted.</param>
        void DeleteTopic(Topic topic);

        /// <summary>
        /// Get a topic by its unique ID.
        /// </summary>
        /// <param name="topicID">The unique ID of the topic.</param>
        /// <returns>A topic entity.</returns>
        Topic GetTopic(long topicID);

        /// <summary>
        /// Get all topics on the specified board.
        /// </summary>
        /// <param name="boardID">The unique ID of the board.</param>
        /// <param name="page">The specified page number.</param>
        /// <param name="itemsPerPage">The number of items to display per page.</param>
        /// <param name="isModerator">True if moderator-only topics should be included.</param>
        /// <returns>An enumerable list of topics.</returns>
        CollectionPage<Topic> GetTopics(short boardID, int page, int itemsPerPage, bool isModerator);

        long AllTopicsCount();

        ForumStats GetForumStats();
    }
}