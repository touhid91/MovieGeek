using System;
using HtmlAgilityPack;

namespace MovieGeek.a1.Helpers
{
    internal static class ParseHelper
    {
        internal static HtmlNodeCollection TrySelectNodes(this HtmlNode htmlNode, String xPath)
        {
            try
            {
                return htmlNode.SelectNodes(xPath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static String TryGetAttribute(this HtmlNode htmlNode, String xPath, String attribute)
        {
            try
            {
                return htmlNode.SelectSingleNode(xPath).Attributes[attribute].Value.CleanUp();
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        internal static String TryGetInnerText(this HtmlNode htmlNode, String xPath)
        {
            try
            {
                return xPath.Equals(String.Empty) ? htmlNode.InnerText.Trim() : htmlNode.SelectSingleNode(xPath).InnerText.Trim();
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        internal static String TryGetInnerHtml(this HtmlNode htmlNode, String xPath)
        {
            try
            {
                return htmlNode.SelectSingleNode(xPath).InnerHtml.Trim();
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }
    }
}