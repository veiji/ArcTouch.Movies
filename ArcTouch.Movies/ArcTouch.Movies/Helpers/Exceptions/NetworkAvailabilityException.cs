using System;
using System.Collections.Generic;
using System.Text;

namespace ArcTouch.Movies.Helpers.Exceptions
{
    public class NetworkAvailabilityException : Exception
    {
        public NetworkAvailabilityException(string message):base(message){}
    }
}
