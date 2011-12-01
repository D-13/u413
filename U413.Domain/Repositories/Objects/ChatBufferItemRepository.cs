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
using U413.Domain.Repositories.Interfaces;
using U413.Domain.Entities;

namespace U413.Domain.Repositories.Objects
{
    /// <summary>
    /// Repistory for persisting chat buffer items to the Entity Framework data context.
    /// </summary>
    public class ChatBufferItemRepository : IChatBufferItemRepository
    {
       /// <summary>
        /// Every repository requires an instance of the Entity Framework data context.
        /// </summary>
        EntityContainer _entityContainer;

        public ChatBufferItemRepository(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        /// <summary>
        /// Adds a chat buffer item to the data context.
        /// </summary>
        /// <param name="chatBufferItem">The chat buffer item to be added.</param>
        public void AddChatBufferItem(ChatBufferItem chatBufferItem)
        {
            _entityContainer.ChatBuffer.Add(chatBufferItem);
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Updates an existing chat buffer item in the data context.
        /// </summary>
        /// <param name="chatBufferItem">The chat buffer item to be updated.</param>
        public void UpdateChatBufferItem(ChatBufferItem chatBufferItem)
        {
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Deletes a chat buffer item from the data context.
        /// </summary>
        /// <param name="chatBufferItem">The chat buffer item to be deleted.</param>
        public void DeleteChatBufferItem(ChatBufferItem chatBufferItem)
        {
            _entityContainer.ChatBuffer.Remove(chatBufferItem);
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Retrieve all new chat buffer items from the data context for a channel by the last received ID.
        /// </summary>
        /// <param name="chatBufferItemID">The last received ID.</param>
        /// <param name="channel">The channel name.</param>
        /// <returns>An enumerable list of chat buffer items</returns>
        public IEnumerable<ChatBufferItem> GetNewChatBufferItemsBy_IDAndChannel(long chatBufferItemID, string channel)
        {
            var query = _entityContainer.ChatBuffer.Where(x => x.ID > chatBufferItemID && x.Channel.ToLower() == channel.ToLower());
            return query;
        }

        /// <summary>
        /// Get last chat buffer item ID from the data context by channel.
        /// </summary>
        /// <param name="channel">The name of the channel.</param>
        /// <returns>A unique ID for the latest chat buffer item in the channel.</returns>
        public long GetLastChatBufferItemIDBy_Channel(string channel)
        {
            var query = _entityContainer.ChatBuffer.Where(x => x.Channel.ToLower() == channel.ToLower());
            long lastChatBufferItemID = 0;
            if (query.Count() > 0)
                lastChatBufferItemID = query.Max(x => x.ID);
            return lastChatBufferItemID;
        }
    }
}