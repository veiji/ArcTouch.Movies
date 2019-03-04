using ArcTouch.Movies.Helpers.Navigation;
using ArcTouch.Movies.Models;
using System;
using System.Threading.Tasks;

namespace ArcTouch.Movies.ViewModels
{
    public class MovieDetailsViewModel : ViewModelBase
    {
        public MovieDetailsViewModel(INavigationService navigation, Func<string, string, string, Task> displayAlertFunc): base(navigation,displayAlertFunc) 
        {
        }

        private Movie movie;
        public Movie Movie
        {
            get => movie; 
            set { SetProperty<Movie>(ref movie,value); }
        }
    }
}
