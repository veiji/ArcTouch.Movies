using ArcTouch.Movies.Models;
using ArcTouch.Movies.ViewModels;
using Xamarin.Forms;

namespace ArcTouch.Movies.Views
{
    public partial class MovieDetails : ContentPage
	{
		public MovieDetails(Movie movie)
		{
			InitializeComponent ();

            MovieDetailsViewModel vm = new MovieDetailsViewModel(App.NavigationService, DisplayAlert)
            {
                Movie = movie
            };
            this.BindingContext = vm;
        }
	}
}