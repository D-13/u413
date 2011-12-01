using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Xml.Linq;

namespace U413.MvcUI.Controllers
{
    public class RSSController : Controller
    {
        public ContentResult HuffAndStapes()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.heidiandfrank.com/podcast");
            request.UserAgent = "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.186 Safari/535.1";
            XDocument xmlDoc;
            using (var response = request.GetResponse())
                xmlDoc = XDocument.Load(response.GetResponseStream());
            xmlDoc.Elements().First().Elements().First().Elements().Single(x => x.Name.LocalName.Equals("title")).Value = "Huff & Stapes";
            xmlDoc.Elements().First().Elements().First().Elements().First(x => x.Name.LocalName.Equals("image")).Descendants("title").First().Value = "Huff & Stapes";
            xmlDoc.Elements().First().Elements().First().Elements().First(x => x.Name.LocalName.Equals("image")).Descendants("url").First().Value = "http://static.huffandstapes.com/podcast/huffstapespodcast.png";
            xmlDoc.Elements().First().Elements().First().Elements().Last(x => x.Name.LocalName.Equals("image")).Attribute("href").Value = "http://static.huffandstapes.com/podcast/huffstapespodcast.png";
            xmlDoc.Descendants("item").Where(x => x.Descendants("title").All(y => !y.Value.StartsWith("Huff & Stapes"))).Remove();
            return this.Content(xmlDoc.ToString(), "text/xml");
        }
    }
}
