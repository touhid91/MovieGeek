using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace MovieGeek.a1.Model
{
    public partial class Movie
    {


        public String Group
        {
            get
            {
                return _group;
            }
            set
            {
                if (value.Contains("&nbsp;"))
                    value = value.Replace("&nbsp;", "");
                if (value.Contains(" - "))
                    value = value.Replace(" - ", ": ");
                _group = value;
            }
        }


        public String Title
        {
            get
            {
                //return _title.Substring(0, _title.Length - 6).Trim();
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public String ImdbUrl { get { return _imdbUrl; } 
            set { _imdbUrl = value.Substring(7, 9); } }

        public String PosterUri
        {
            get
            {
                return _posterUri;
            }
            set
            {
                //var pattern = "_CR{0},0,140,209_";
                //var tempVal = value;
                //for (Int32 i = 0; i < 10; i++)
                //{
                //    if (tempVal.Equals(value))
                //        tempVal = value.Replace(String.Format(pattern, i), "");
                //}
                //value = tempVal;
                //if (value.Contains("@._V1_SY"))
                //    _posterUri = value.Replace("_SY209", "_SY418");
                //else if (value.Contains("@._V1_SX"))
                //    _posterUri =
                //        value.Replace("_SX140", "_SX280");
                _posterUri = value;
            }
        }

        public String ReleaseDate
        {
            get
            {
                return _releaseDate ?? (_releaseDate = _title.Substring(_title.Length - 5, 4));
            }
            set
            {
                _releaseDate = value;
            }
        }

        public String Runtime
        {
            get;
            set;
        }

        public String Synopsis
        {
            get;
            set;
        }

        public String MpaaRating
        {
            get
            {
                return _mpaaRating;
            }
            set
            {
                //if (value != String.Empty)
                //    if (value.Contains("Certificate "))
                //    {
                //        value = value.Replace("Certificate ", "");
                //    }
                _mpaaRating = value;
            }
        }

        public SolidColorBrush MpaaRatingBrush
        {
            get
            {
                if (_mpaaRating.Equals("G"))
                    return new SolidColorBrush(Color.FromArgb(255, 10, 148, 68));
                if (_mpaaRating.Equals("PG"))
                    return new SolidColorBrush(Color.FromArgb(255, 241, 89, 42));
                if (_mpaaRating.Equals("PG-13"))
                    return new SolidColorBrush(Color.FromArgb(255, 102, 45, 145));
                if (_mpaaRating.Equals("R"))
                    return new SolidColorBrush(Color.FromArgb(255, 238, 28, 37));
                if (_mpaaRating.Equals("NC-17"))
                    return new SolidColorBrush(Color.FromArgb(255, 14, 118, 137));
                return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }

        public ObservableCollection<Genre> GenreCollection
        {
            get;
            set;
        }

        public Rating Rating
        {
            get;
            set;
        }

        public ImdbReview ImdbReview { get; set; }

        public List<Crew> Directors
        {
            get;
            set;
        }

        public List<Crew> Actors
        {
            get;
            set;
        }
    }

    public partial class Movie
    {
        private String _mpaaRating;
        private String _posterUri;
        private String _releaseDate;
        private String _imdbUrl;
        private String _title;
        private String _group;
    }
}