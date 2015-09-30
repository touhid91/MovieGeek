using System;

namespace MovieGeek.a1.Helpers
{
    public static class StringHelper
    {
        internal static String CleanUp(this String request)
        {
            // TO-DO
            // Implement cleaning
            return request.Trim();
        }

        public static Uri ToUri(this String request)
        {
            return new Uri(request);
        }

        public static Double ToDouble(this String request)
        {
            Double result;
            Double.TryParse(request, out result);
            return result;
        }

        public static String CleanupCommas(this String request)
        {
            request.Replace(", ", "");
            return request;
        }
    }
}