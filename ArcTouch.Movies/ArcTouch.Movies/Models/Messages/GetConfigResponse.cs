using System.Collections.Generic;

namespace ArcTouch.Movies.Models.Messages
{
    public class ImageConfig
    {
        public string BaseUrl{ get; set; }
        public string[] PosterSizes{ get; set; }
        public string[] BackdropSizes { get; set; }
    }

    public class GetConfigResponse
    {
        public ImageConfig Images { get; set; }

    }
}
