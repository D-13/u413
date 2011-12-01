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
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using U413.Domain.Repositories.Interfaces;
using U413.Domain.ExtensionMethods;
using U413.Domain.Settings;
using U413.Domain.Entities;
using System.Drawing;
using System.Net;
using System.IO;

namespace U413.Domain.Utilities
{
    /// <summary>
    /// Utilities for handling tag conversion. The UI can utilize this utility when parsing text containing BBCode tags.
    /// </summary>
    public static class BBCodeUtility
    {
        #region Public Methods

        /// <summary>
        /// Find formatting tags in the text and transform them into the appropriate HTML.
        /// </summary>
        /// <param name="text">The text to be transformed.</param>
        /// <returns>A formatted string.</returns>
        public static string ConvertTagsToHtml(string text)
        {
            text = TransformTags(text, (content, optionalValue, tag) =>
                {
                    tag = tag.ToLower();

                    if (tag.Is("code"))
                        return string.Format("<pre><code>{0}</code></pre>", content);
                    else if (tag.Is("color"))
                        return string.Format("<span style='color: {0}; border-color: {0};'>{1}</span>", optionalValue, content);
                    else if (tag.Is("img"))
                    {
                        string imageUrl = ConfirmHttp(content);
                        try
                        {
                            var client = new WebClient();
                            var stream = client.OpenRead(imageUrl);
                            var bitmap = new Bitmap(stream);
                            stream.Flush();
                            stream.Close();
                            var width = Convert.ToDecimal(bitmap.Size.Width);
                            var height = Convert.ToDecimal(bitmap.Size.Height);
                            if (width > 500m)
                            {
                                var ratio = width / 500m;
                                height = height / ratio;
                                width = 500m;
                            }
                            return string.Format("<div style='height: {0}px; width: {1}px;'><a target='_blank' href='{2}'><img style='height: {0}px; width: {1}px;' src='{2}' /></a></div>", height, width, imageUrl);
                        }
                        catch
                        {
                            return string.Format("<div><a target='_blank' href='{0}'><img src='{0}' /></a></div>", imageUrl);
                        }
                    }
                    else if (tag.Is("url"))
                        return string.Format("<a target='_blank' href='{0}'>{1}</a>", string.IsNullOrEmpty(optionalValue) ? ConfirmHttp(content) : ConfirmHttp(optionalValue), content);
                    else if (tag.Is("transmit"))
                        return string.Format("<span class='transmit'>{0}</span>", content);
                    else if (tag.Is("quote"))
                        return string.Format("<div><div class='quote'>{0}</div></div>", content);
                    else if (tag.Is("b"))
                        return string.Format("<b>{0}</b>", content);
                    else if (tag.Is("i"))
                        return string.Format("<i>{0}</i>", content);
                    else if (tag.Is("u"))
                        return string.Format("<u>{0}</u>", content);
                    else if (tag.Is("s"))
                        return string.Format("<strike>{0}</strike>", content);
                    else
                        return null;
                });

            return text;
        }

        public static string ConvertTagsForConsole(string text)
        {
            text = TransformTags(text, (content, optionalValue, tag) =>
            {
                tag = tag.ToLower();

                if (tag.Is("code"))
                    return string.Format("{{ {0} }}", content);
                else if (tag.Is("color"))
                    return string.Format("{0}", content);
                else if (tag.Is("img"))
                {
                    return string.Format("{0}", content);
                }
                else if (tag.Is("url"))
                    return string.Format("({1}) {0}", string.IsNullOrEmpty(optionalValue) ? ConfirmHttp(content) : ConfirmHttp(optionalValue), content);
                else if (tag.Is("transmit"))
                    return string.Format("{0}", content);
                else if (tag.Is("quote"))
                    return string.Format("\n\"{0}\"\n", content);
                else if (tag.Is("b"))
                    return string.Format("*{0}*", content);
                else if (tag.Is("i"))
                    return string.Format("'{0}'", content);
                else if (tag.Is("u"))
                    return string.Format("_{0}_", content);
                else if (tag.Is("s"))
                    return string.Format("-{0}-", content);
                else
                    return null;
            });

            return text;
        }

        /// <summary>
        /// Find formatting tags in the text and transform them into static tags.
        /// </summary>
        /// <param name="text">The text to be transformed.</param>
        /// <param name="replyRepository">An instance of IReplyRepository.</param>
        /// <param name="isModerator">True if the current user is a moderator.</param>
        /// <returns>A formatted string.</returns>
        public static string SimplifyComplexTags(string text, IReplyRepository replyRepository, bool isModerator)
        {
            text = TransformTags(text, (content, optionalValue, tag) =>
                {
                    if (tag.Is("quote"))
                    {
                        string reformattedQuote = string.Format("[quote]{0}[/quote]", content);
                        if (content.IsLong())
                        {
                            Reply reply = replyRepository.GetReply(content.ToLong());
                            if (reply != null)
                                if (!reply.IsModsOnly() || isModerator)
                                    reformattedQuote = string.Format("[quote][b]Posted by:[/b] [transmit]{0}[/transmit] on {1}\n\n{2}[/quote]", reply.Username, reply.PostedDate, reply.Body);
                        }
                        return reformattedQuote;
                    }
                    else
                        return null;
                });
            return text;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Match all tags in the text with a regular expression.
        /// </summary>
        /// <param name="input">The text to be matched against.</param>
        /// <param name="handleTags">A function to interpret and transform tags.</param>
        /// <returns>Text with transformed tags.</returns>
        private static string TransformTags(string input, Func<string, string, string, string> handleTags)
        {
            var regexOptions = RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline;
            input = Regex.Replace(input, RegularExpressions.BBCodeTags, new MatchEvaluator(match =>
            {
                var originalText = match.Groups[0].Value;
                var tag = match.Groups[1].Value;
                var optionalValue = match.Groups[2].Value;
                var content = match.Groups[3].Value;
                content = TransformTags(content, handleTags);
                content = handleTags(content, optionalValue, tag) ?? originalText;
                return content;
            }), regexOptions);

            return input;
        }

        /// <summary>
        /// Checks if a string begins with http. If not, it adds it.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>URL complete with http://</returns>
        private static string ConfirmHttp(string url)
        {
            return new string[] { "http://", "https://", "ftp://", "mailto:" }.Any(x => url.StartsWith(x, true, System.Globalization.CultureInfo.CurrentCulture)) ? url : "http://" + url;
        }

        #endregion
    }
}
