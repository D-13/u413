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
    /// Repository for storing short URLs.
    /// </summary>
    public interface IShortUrlRepository
    {
        /// <summary>
        /// Add short URL to repository.
        /// </summary>
        /// <param name="shortUrl">The short URL to be added.</param>
        void AddShortUrl(ShortURL shortUrl);

        /// <summary>
        /// Delete short URL from the repository.
        /// </summary>
        /// <param name="shortUrl">The short URL to be deleted.</param>
        void DeleteShortUrl(ShortURL shortUrl);

        /// <summary>
        /// Get short URL by it's unique ID.
        /// </summary>
        /// <param name="shortUrlID">The unique ID of the short URL.</param>
        /// <returns>A short URL entity.</returns>
        ShortURL GetShortUrl(long shortUrlID);
    }
}