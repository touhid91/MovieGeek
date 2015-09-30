using System;
using System.Collections.ObjectModel;
using System.Linq;
using HtmlAgilityPack;
using MovieGeek.a1.Model;

namespace MovieGeek.a1.Helpers
{
    internal static class ImdbHelper
    {
        //Box Office Top Ten
        internal static ObservableCollection<Movie> ParseTopBoxOffice(this HtmlNode htmlNode)
        {
            var movies = htmlNode.TrySelectNodes("//*[@id='main']/div/div[3]//div[starts-with(@class,'list_item')]")
                .Select(m => new Movie
                {
                    Group = m.ParentNode.TryGetInnerText("//h3").Trim(),
                    Title = m.TryGetAttribute(".//h4[@itemprop='name']/a", "title"),
                    PosterUri = m.TryGetAttribute(".//img[@itemprop='image']", "src"),
                    ImdbUrl = m.TryGetAttribute(".//h4[@itemprop='name']/a", "href"),
                    Runtime = m.TryGetInnerText(".//p/time[@itemprop='duration']"),
                    Synopsis = m.TryGetInnerText(".//div[@itemprop='description']"),
                    GenreCollection = new ObservableCollection<Genre>(
                        m.TrySelectNodes(".//span[@itemprop='genre']")
                            .Select(g => new Genre
                            {
                                CategoryTitle = g.TryGetInnerText(String.Empty)
                            }).ToObservableCollection()),
                    MpaaRating = m.TryGetAttribute(".//p[@class='cert-runtime-genre']//img", "alt").Trim().Replace("Certificate ",""),

                    Rating = new Rating
                    {
                        ImdbRating = new ImdbRating
                        {
                            RatingValue = m.TryGetAttribute(".//meta[@itemprop='ratingValue']", "content").ToDouble()/2,
                            MetaCritic = new MetaCritic
                            {
                                MetaScore = m.TryGetInnerText(".//div[@class='rating_txt']//strong").ToDouble()
                            }
                        }
                    }
                }).ToObservableCollection();
            return movies;
        }

        //Opening This Week
        internal static ObservableCollection<Movie> ParseUpcoming(this HtmlNode htmlNode)
        {
            var movies = htmlNode.TrySelectNodes("//*[@id='main']/div/div[2]//div[starts-with(@class,'list_item')]")
                .Select(m => new Movie
                {
                    Group = m.ParentNode.TryGetInnerText("//h3").Trim(),
                    Title = m.TryGetInnerText(".//h4[@itemprop='name']/a"),
                    ImdbUrl = m.TryGetAttribute(".//h4[@itemprop='name']/a", "href"),
                    PosterUri = m.TryGetAttribute(".//img[@itemprop='image']", "src"),
                    Runtime = m.TryGetInnerText(".//p/time[@itemprop='duration']"),
                    Synopsis = m.TryGetInnerText(".//div[@itemprop='description']"),
                    GenreCollection = new ObservableCollection<Genre>(
                           m.TrySelectNodes(".//span[@itemprop='genre']")
                               .Select(g => new Genre
                               {
                                   CategoryTitle = g.TryGetInnerText(String.Empty)
                               }).ToObservableCollection()),
                    MpaaRating = m.TryGetAttribute(".//p[@class='cert-runtime-genre']//img", "title").Trim(),
                    Rating = new Rating
                    {
                        ImdbRating = new ImdbRating
                        {
                            RatingValue = m.TryGetAttribute(".//meta[@itemprop='ratingValue']", "content").ToDouble(),
                            MetaCritic = new MetaCritic
                            {
                                MetaScore = m.TryGetInnerText(".//div[@class='rating_txt']//strong").ToDouble()
                            }
                        }
                    },
                    Directors = m.TrySelectNodes(".//span[@itemprop='director']").Select(d => new Crew
                    {
                        Name = d.TryGetInnerText(".//span[@itemprop='name']/a"),
                        Affiliation = "Director",
                        RefUrl = d.TryGetAttribute(".//span[@itemprop='name']/a", "href")
                    }).ToList()
                }).ToObservableCollection();
            return movies;
        }

        internal static Movie ParseMovie(this HtmlNode htmlNode)
        {
            var movie = new Movie()
            {
                Title = htmlNode.TryGetInnerText(".//span[@itemprop='name']"),
                ReleaseDate = htmlNode.TryGetInnerText(".//span[@class='nobr']/a"),
                MpaaRating = htmlNode.TryGetAttribute(".//span[@itemprop='contentRating']", "content"),
                PosterUri = htmlNode.TryGetAttribute(".//img[@itemprop='image']", "src"),
                Runtime = htmlNode.TryGetInnerText(".//time[@itemprop='duration']"),
                Synopsis = htmlNode.TryGetInnerText("//p[@itemprop='description']"),
                Rating = new Rating()
                {
                    ImdbRating = new ImdbRating()
                    {
                        UserCount = "From " + htmlNode.TryGetInnerText("//*[@id='overview-top']/div[3]/div[3]/a[1]/span") + " users",
                        RatingValue = Double.Parse(htmlNode.TryGetInnerText("//*[@id='overview-top']/div[3]/div[1]").Trim()) / 2
                    }
                },
                Actors = htmlNode.TrySelectNodes("//*[@id='titleCast']/table//tr").Select(a => new Crew
                {
                    Affiliation = "Actor",
                    Name = a.TryGetInnerText(".//span[@itemprop='name']"),
                    PotraitUri = a.TryGetAttribute(".//td[@class='primary_photo']//img", "src"),
                    RolePlay = a.TryGetInnerText(".//td[@class='character']//div")
                }).ToList(),
                ImdbReview = new ImdbReview
                {
                    Author = htmlNode.TryGetInnerText(".//div[@class='comment-meta']//span[@itemprop='author']"),
                    Headline = htmlNode.TryGetInnerText("//*[@id='titleUserReviewsTeaser']/div/span/strong"),
                    ReviewBody = htmlNode.TryGetInnerText("//*[@id='titleUserReviewsTeaser']/div/span/div[2]/p")
                }
            };
            movie.Actors.RemoveAt(0);
            return movie;
        }

        internal static ObservableCollection<Crew> ParseCrews(this HtmlNode htmlNode)
        {
            var stars = htmlNode.TrySelectNodes("//table[@class='results']/tr").Select(c => new Crew()
            {
                Name = c.TryGetInnerText(".//td[@class='name']/a"),
                Affiliation = c.TryGetInnerText(".//span[@class='description']"),
                KnownFor = c.TryGetInnerText(".//span[@class='description']/a"),
                PotraitUri=c.TryGetAttribute(".//td[@class='image']/a/img","src")
            }).ToObservableCollection();
            return stars;
        }

        public static ObservableCollection<SearchRes> SearchParser(this HtmlNode htmlNode)
        {
            var searchResults = htmlNode.TrySelectNodes(".//tr[starts-with(@class,'findResult')]").Select(s => new SearchRes
            {
                Title = s.TryGetInnerText(".//td[@class='result_text']/a"),
                //Extra= s.TryGetInnerText("//td/small")
                PosterUri = s.TryGetAttribute(".//td[@class='primary_photo']/a/img","src"),
                SelfUri = s.TryGetAttribute(".//td[@class='result_text']/a","href").Substring(7,9)
            }).ToObservableCollection();
            return searchResults;
        }
    }
}