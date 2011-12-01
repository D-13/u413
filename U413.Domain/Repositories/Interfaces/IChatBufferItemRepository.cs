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
    /// Repository for storing chat buffer items.
    /// </summary>
    public interface IChatBufferItemRepository
    {
        /// <summary>
        /// Adds a chat buffer item to the repository.
        /// </summary>
        /// <param name="chatBufferItem">The chat buffer item to be added.</param>
        void AddChatBufferItem(ChatBufferItem chatBufferItem);

        /// <summary>
        /// Updates a chat buffer item in the repository.
        /// </summary>
        /// <param name="chatBufferItem">The chat buffer item to be updated.</param>
        void UpdateChatBufferItem (ChatBufferItem chatBufferItem);

        /// <summary>
        /// Delete a chat buffer item from the repository.
        /// </summary>
        /// <param name="chatBufferItem">The chat buffer item to be deleted.</param>
        void DeleteChatBufferItem(ChatBufferItem chatBufferItem);

        /// <summary>
        /// Obtain all new chat buffer items since specified chat buffer item ID.
        /// </summary>
        /// <param name="chatBufferItemID">The unique ID of the last received item.</param>
        /// <param name="channel">The name of the channel.</param>
        /// <returns>An enumerable list of chat buffer items.</returns>
        IEnumerable<ChatBufferItem> GetNewChatBufferItemsBy_IDAndChannel(long chatBufferItemID, string channel);

        /// <summary>
        /// Get the last chat buffer item ID by channel name.
        /// </summary>
        /// <param name="channel">The name of the channel.</param>
        /// <returns>A 64-bit integer unique ID.</returns>
        long GetLastChatBufferItemIDBy_Channel(string channel);
    }
}