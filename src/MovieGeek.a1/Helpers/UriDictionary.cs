using System;

namespace MovieGeek.a1.Helpers
{
    internal static class UriDictionary
    {
        private const String InTheaters = "http://www.imdb.com/movies-in-theaters";
        private const String LatestTrailers = "http://www.imdb.com/trailers";
        private const String PopularByGenre = "http://www.imdb.com/genre";

        internal static Uri InTheatersUri
        {
            get
            {
                return new Uri(InTheaters);
            }
        }

        internal static Uri LatestTrailersUri
        {
            get
            {
                return new Uri(LatestTrailers);
            }
        }

        internal static Uri PopularByGenreUri
        {
            get
            {
                return new Uri(PopularByGenre);
            }
        }
    }
}