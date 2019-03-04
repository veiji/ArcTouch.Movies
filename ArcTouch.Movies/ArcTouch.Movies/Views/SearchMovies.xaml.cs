using System.Collections.Generic;
using ArcTouch.Movies.Helpers.Communication;
using ArcTouch.Movies.ViewModels;
using Xamarin.Forms;

namespace ArcTouch.Movies.Views
{
    public partial class SearchMovies : ContentPage
	{
		public SearchMovies (Dictionary<int,string> genreCache)
		{
			InitializeComponent ();
            
            this.BindingContext = new SearchMoviesViewModel(App.NavigationService, DisplayAlert, DependencyService.Get<IRequestHelper>(), genreCache);
        }
	}
}