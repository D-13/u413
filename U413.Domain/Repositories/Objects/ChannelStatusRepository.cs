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
    /// Repository for persisting channel statuses to the Entity Framework data context.
    /// </summary>
    public class ChannelStatusRepository : IChannelStatusRepository
    {
        /// <summary>
        /// Every repository requires an instance of the Entity Framework data context.
        /// </summary>
        EntityContainer _entityContainer;

        public ChannelStatusRepository(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        /// <summary>
        /// Get channel status from the data context by channel and username.
        /// </summary>
        /// <param name="channel">The name of the channel.</param>
        /// <param name="username">The name of the user.</param>
        /// <returns>A channel status entity.</returns>
        public ChannelStatus GetChannelStatusBy_ChannelAndUsername(string channel, string username)
        {
            var query = _entityContainer.ChannelStatuses.Where(x => x.Channel.ToLower() == channel.ToLower() && x.Username.ToLower() == username.ToLower()).FirstOrDefault();
            return query;
        }

        /// <summary>
        /// Adds a channel status to the data context.
        /// </summary>
        /// <param name="channelStatus">The channel status to be added.</param>
        public void AddChannelStatus(ChannelStatus channelStatus)
        {
            _entityContainer.ChannelStatuses.Add(channelStatus);
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Update an existing channel status in the data context.
        /// </summary>
        public void UpdateChannelStatus()
        {
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Delete a channel status from the data context.
        /// </summary>
        /// <param name="channelStatus">The channel status to be deleted.</param>
        public void DeleteChannelStatus(ChannelStatus channelStatus)
        {
            _entityContainer.ChannelStatuses.Remove(channelStatus);
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Retrieve channel statuses from the data context by username.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns>An enumerable list of channel statuses.</returns>
        public IEnumerable<ChannelStatus> GetOpenChannelsBy_Username(string username)
        {
            var query = _entityContainer.ChannelStatuses.Where(x => x.Username.ToLower() == username.ToLower());
            return query;
        }

        /// <summary>
        /// Retrieve active channel statuses from the data context by channel.
        /// </summary>
        /// <param name="channel">The channel name.</param>
        /// <returns>An enumerable list of channel statuses.</returns>
        public IEnumerable<ChannelStatus> GetActiveUsersBy_Channel(string channel)
        {
            DateTime tenMinutesAgo = DateTime.UtcNow.AddMinutes(-10);
            var query = _entityContainer.ChannelStatuses.Where(x => x.Channel.ToLower() == channel.ToLower() && x.LastActive > tenMinutesAgo);
            return query;
        }

        /// <summary>
        /// Retrieve idle channel statuses from the data context by channel.
        /// </summary>
        /// <param name="channel">The name of the channel.</param>
        /// <returns>An enumerable list of channel statuses.</returns>
        public IEnumerable<ChannelStatus> GetIdleUsersBy_Channel(string channel)
        {
            DateTime tenMinutesAgo = DateTime.UtcNow.AddMinutes(-10);
            var query = _entityContainer.ChannelStatuses.Where(x => x.Channel.ToLower() == channel.ToLower() && x.LastSeen > tenMinutesAgo && x.LastActive < tenMinutesAgo);
            return query;
        }

        /// <summary>
        /// Retrieve all active channel statuses from the data context.
        /// </summary>
        /// <returns>An enumerable list of channel statuses.</returns>
        public IEnumerable<ChannelStatus> GetActiveChannels()
        {
            DateTime tenMinutesAgo = DateTime.UtcNow.AddMinutes(-10);
            var query = _entityContainer.ChannelStatuses.Where(x => x.LastSeen > tenMinutesAgo);
            return query;
        }

        /// <summary>
        /// Determine if a channel is open.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="channel">The name of the channel.</param>
        /// <returns>True if the channel is already open for that user.</returns>
        public bool ChannelOpen(string username, string channel)
        {
            var query = _entityContainer.ChannelStatuses.Any(x => x.Username.ToLower() == username.ToLower() && x.Channel.ToLower() == channel.ToLower());
            return query;
        }
    }
}