using Newtonsoft.Json;
using System;

namespace ArcTouch.Movies.Models
{
    public class Movie
    {
        public string PosterPath { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public int[] GenreIds { get; set; }
        public int Id { get; set; }
        public string OriginalTitle { get; set; }
        public string OriginalLanguage { get; set; }
        public string Title { get; set; }
        public string BackdropPath { get; set; }
        public double Popularity { get; set; }
        public int VoteCount { get; set; }
        public bool Video { get; set; }
        public double VoteAverage { get; set; }

        [JsonIgnore]
        public string Genre { get; set; }

        [JsonIgnore]
        public string FullPosterPath { get; set; }

        [JsonIgnore]
        public string FullBackdropPath { get; set; }

        public string ReleaseDateFormatted
        {
            get
            {
                if(DateTime.TryParse(ReleaseDate, out DateTime release))
                    return release.ToString("MMM dd yyyy");
                return string.Empty;
            }
        }

    }




}
