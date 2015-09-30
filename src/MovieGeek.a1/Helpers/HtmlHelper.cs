using System;
using HtmlAgilityPack;

namespace MovieGeek.a1.Helpers
{
    public static class HtmlHelper
    {
        public static HtmlNode CleanUp(this HtmlNode request)
        {
            try
            {
                var scriptNodes = request.SelectNodes(".//script");
                foreach (var scriptNode in scriptNodes)
                {
                    scriptNode.Remove();
                }
            }
            catch (NullReferenceException)
            {
            }
            return request;
        }

        public static HtmlNode CreateDocument(this String rawHtml)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(rawHtml);
            return htmlDocument.DocumentNode.CleanUp();
        }
    }
}