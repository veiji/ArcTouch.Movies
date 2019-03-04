# ArcTouch.Movies
Simple Xamarin.Forms App to list and see details of some movies.

Instructions:

Just put your TMDb key in ArcTouch.Movies/Properties/Configurations.resx, ApiKey key name.

Third-Party libraries used:

Newtonsoft.Json: Library used to deserialize the responses of TMDb api calls.
sqlite-net-pcl: Used for sqlite abstraction in PCL.
FFImageLoading: Provides CachedImage class, used to render images like Xamarin.Forms.Image does but with some additional properties.

Additional Informations:

It was only tested on an Android device.
iPhone version may not behave as expected.