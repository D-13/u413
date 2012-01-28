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
    /// A repository to store channel statuses.
    /// </summary>
    public interface IChannelStatusRepository
    {
        /// <summary>
        /// Get channel status by channel and username.
        /// </summary>
        /// <param name="channel">The name of the channel.</param>
        /// <param name="username">The name of the user.</param>
        /// <returns>A channel status entity.</returns>
        ChannelStatus GetChannelStatusBy_ChannelAndUsername(string channel, string username);

        /// <summary>
        /// Adds a channel status to the repository.
        /// </summary>
        /// <param name="channelStatus">The channel status to be added.</param>
        void AddChannelStatus(ChannelStatus channelStatus);

        /// <summary>
        /// Updates an existing channel status in the repository.
        /// </summary>
        void UpdateChannelStatus();

        /// <summary>
        /// Deletes a channel status from the repository.
        /// </summary>
        /// <param name="channelStatus">The channel status to be deleted.</param>
        void DeleteChannelStatus(ChannelStatus channelStatus);

        /// <summary>
        /// Get a list of channel statuses by username.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns>An enumerable list of channel statuses.</returns>
        IEnumerable<ChannelStatus> GetOpenChannelsBy_Username(string username);

        /// <summary>
        /// Get a list of active channel statuses by channel.
        /// </summary>
        /// <param name="channel">The channel name.</param>
        /// <returns>An enumerable list of channel statuses.</returns>
        IEnumerable<ChannelStatus> GetActiveUsersBy_Channel(string channel);

        /// <summary>
        /// Get a list of idle channel statuses by channel.
        /// </summary>
        /// <param name="channel">The channel name.</param>
        /// <returns>An enumerable list of channel statuses.</returns>
        IEnumerable<ChannelStatus> GetIdleUsersBy_Channel(string channel);

        /// <summary>
        /// Get all active channel statuses.
        /// </summary>
        /// <returns>An enumerable list of channel statuses.</returns>
        IEnumerable<ChannelStatus> GetActiveChannels();

        /// <summary>
        /// Check if a channel is open.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="channel">The channel name.</param>
        /// <returns>True if the channel is open.</returns>
        bool ChannelOpen(string username, string channel);
    }
}