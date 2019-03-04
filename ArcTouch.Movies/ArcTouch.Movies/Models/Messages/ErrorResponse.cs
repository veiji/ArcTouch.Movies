namespace ArcTouch.Movies.Models.Messages
{
    public class ErrorResponse
    {
        public string StatusMessage { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string[] Errors { get; set; }
    }
}
