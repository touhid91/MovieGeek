using System;
using System.Windows.Media;

namespace MovieGeek.a1.Model
{
    public class Rating
    {
        public ImdbRating ImdbRating { get; set; }
    }

    public class ImdbRating
    {
        public Double RatingValue { get; set; }
        public String UserCount { get; set; }
        public Double CriticCount { get; set; }
        public MetaCritic MetaCritic { get; set; }
        public MovieMeter MovieMeterStatus { get; set; }
    }

    public class MetaCritic
    {
        public Double? MetaScore { get; set; }

        public SolidColorBrush MetaScoreBrush {
            get
            {
                if (MetaScore > 60) return new SolidColorBrush(Color.FromArgb(255, 102, 204, 51));
                if (MetaScore > 39) return new SolidColorBrush(Color.FromArgb(255, 255, 204, 51));
                return new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }
        }
        public Double? CriticCount { get; set; }
        public Double? UserScore { get; set; }
        public Double? UserCount { get; set; }
    }

    public class MovieMeter
    {
        public Boolean? IsOnTop100 { get; set; }
        public Int16? Standing { get; set; }
        public MeterDirection? MeterDirection { get; set; }
        public Int16? MeterChangeValue { get; set; }
    }

    public enum MeterDirection
    {
        Up,
        Neutral,
        Down
    }
}