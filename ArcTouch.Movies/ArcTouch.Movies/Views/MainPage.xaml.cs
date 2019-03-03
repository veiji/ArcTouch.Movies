using ArcTouch.Movies.Helpers.Communication;
using ArcTouch.Movies.Repository;
using ArcTouch.Movies.ViewModels;

using Xamarin.Forms;

namespace ArcTouch.Movies.Views
{
    public partial class MainPage : ContentPage
	{
        public MainPage()
        {
            InitializeComponent();

            MainViewModel vm = new MainViewModel(App.NavigationService, DisplayAlert, DependencyService.Get<IRequestHelper>(), DependencyService.Get<IMoviesRepository>());
            this.BindingContext = vm;
		}
	}
}