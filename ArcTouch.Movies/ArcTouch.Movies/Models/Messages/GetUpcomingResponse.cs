using System.Collections.Generic;

namespace ArcTouch.Movies.Models.Messages
{
    public class GetUpcomingResponse
    {
        public int Page { get; set; }
        public List<Movie> Results { get; set; }
        public MovieDates Dates { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }

    public class MovieDates
    {
        public string Maximum { get; set; }
        public string Minimum { get; set; }
    }
}
