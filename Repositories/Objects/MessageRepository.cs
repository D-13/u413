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
using System.Linq.Expressions;
using U413.Domain.Repositories.Interfaces;
using U413.Domain.Entities;
using U413.Domain.Objects;
using U413.Domain.ExtensionMethods;

namespace U413.Domain.Repositories.Objects
{
    /// <summary>
    /// Repository for persisting messages to the Entity Framework data context.
    /// </summary>
    public class MessageRepository : IMessageRepository
    {
        #region Dependencies

        /// <summary>
        /// Every repository requires an instance of the Entity Framework data context.
        /// </summary>
        EntityContainer _entityContainer;

        public MessageRepository(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        #endregion

        #region Interface Members

        /// <summary>
        /// Retrieves a message fromt he data context by its unique ID.
        /// </summary>
        /// <param name="messageId">The unique ID of the message.</param>
        /// <returns>A message entity.</returns>
        public Message GetMessage(long messageId)
        {
            var query = _entityContainer.Messages.SingleOrDefault(x => x.MessageID == messageId);
            return query;
        }

        /// <summary>
        /// Retrieves a page of messages from the data context for a user.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="page">The page number.</param>
        /// <param name="itemsPerPage">The number of items to display per page.</param>
        /// <param name="sent">True if retrieving sent messages.</param>
        /// <returns>A collection page of messages.</returns>
        public CollectionPage<Message> GetMessages(string username, int page, int itemsPerPage, bool sent)
        {
            var messages = _entityContainer.Messages.AsQueryable();
            messages = messages
                .Where(MessageBelongsToUser(username, sent))
                .Where(UserDidNotDeleteMessage(username, sent))
                .OrderByDescending(x => x.SentDate);

            int totalMessages = messages.Count();

            int totalPages = totalMessages.NumberOfPages(itemsPerPage);
            if (totalPages <= 0)
                totalPages = 1;

            if (page > totalPages)
                return GetMessages(username, totalPages, itemsPerPage, sent);
            else if (page < 1)
                return GetMessages(username, 1, itemsPerPage, sent);
            else
            {
                if (totalMessages > itemsPerPage)
                    messages = messages
                        .Skip(itemsPerPage * (page - 1))
                        .Take(itemsPerPage)
                        .OrderByDescending(x => x.SentDate);

                return new CollectionPage<Message>
                {
                    TotalItems = totalMessages,
                    TotalPages = totalMessages.NumberOfPages(itemsPerPage),
                    Items = messages.AsEnumerable().Reverse().ToList()
                };
            }
        }

        public int UnreadMessages(string username)
        {
            return _entityContainer.Messages
                .Where(MessageBelongsToUser(username, false))
                .Where(UserDidNotDeleteMessage(username, false))
                .Count(x => !x.MessageRead);
        }

        /// <summary>
        /// Retrieves a list of all messages from the data context for user.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="sent">True if retrieving sent messages.</param>
        /// <returns>An enumerable list of messages.</returns>
        public IEnumerable<Message> GetAllMessages(string username, bool sent)
        {
            return _entityContainer.Messages
                .Where(MessageBelongsToUser(username, sent))
                .Where(UserDidNotDeleteMessage(username, sent))
                .OrderByDescending(x => x.SentDate);
        }

        /// <summary>
        /// Adds a message to the data context.
        /// </summary>
        /// <param name="message">The message to be added.</param>
        public void AddMessage(Message message)
        {
            _entityContainer.Messages.Add(message);
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// This method is not used. Please just call SaveChanges().
        /// </summary>
        /// <param name="message">The message to be updated.</param>
        [Obsolete]
        public void UpdateMessage(Message message)
        {
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Deletes a message from the data context.
        /// </summary>
        /// <param name="message">The message to be deleted.</param>
        public void DeleteMessage(Message message)
        {
            _entityContainer.Messages.Remove(message);
            _entityContainer.SaveChanges();
        }

        #endregion

        #region Expressions

        /// <summary>
        /// Expression to determine if a message belongs to a user.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="sent">True if retrieving sent messages.</param>
        /// <returns>An expression to be used in a LINQ query.</returns>
        private Expression<Func<Message, bool>> MessageBelongsToUser(string username, bool sent)
        {
            return x => (sent ? x.FromUser : x.ToUser).Username.Equals(username, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Expression to determine if a message has been deleted by the user.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="sent">True if retrieving sent messages.</param>
        /// <returns>An expression to be used in a LINQ query.</returns>
        private Expression<Func<Message, bool>> UserDidNotDeleteMessage(string username, bool sent)
        {
            return x => sent ? !x.SenderDeleted : !x.RecipientDeleted;
        }

        #endregion
    }
}