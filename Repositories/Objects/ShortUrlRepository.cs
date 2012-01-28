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
    /// Repository for persisting short URLs to the Entity Framework data context.
    /// </summary>
    public class ShortUrlRepository : IShortUrlRepository
    {
        /// <summary>
        /// Every repository requires an instance of the Entity Framework data context.
        /// </summary>
        EntityContainer _entityContainer;

        public ShortUrlRepository(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        /// <summary>
        /// Adds a short URL to the data context.
        /// </summary>
        /// <param name="shortUrl">The short URL to be added.</param>
        public void AddShortUrl(ShortURL shortUrl)
        {
            _entityContainer.ShortURLs.Add(shortUrl);
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Deletes a short URL from the data context.
        /// </summary>
        /// <param name="shortUrl">The short URL to be deleted.</param>
        public void DeleteShortUrl(ShortURL shortUrl)
        {
            _entityContainer.ShortURLs.Remove(shortUrl);
            _entityContainer.SaveChanges();
        }

        /// <summary>
        /// Gets a short URL from the data context by its unique ID.
        /// </summary>
        /// <param name="shortUrlID">The unique ID of the short URL.</param>
        /// <returns>A short URL entity.</returns>
        public ShortURL GetShortUrl(long shortUrlID)
        {
            var query = _entityContainer.ShortURLs.Where(x => x.UrlID == shortUrlID).FirstOrDefault();
            return query;
        }
    }
}