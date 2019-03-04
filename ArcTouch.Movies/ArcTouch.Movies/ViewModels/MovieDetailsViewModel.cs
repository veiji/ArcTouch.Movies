using ArcTouch.Movies.Helpers.Navigation;
using ArcTouch.Movies.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArcTouch.Movies.ViewModels
{
    public class MovieDetailsViewModel : ViewModelBase
    {
        public MovieDetailsViewModel(INavigationService navigation, Func<string, string, string, Task> displayAlertFunc): base(navigation,displayAlertFunc) 
        {
        }

        ICommand backCmd;
        public ICommand BackCmd => backCmd ?? (backCmd = new Command(GoBack));

        private Movie movie;
        public Movie Movie
        {
            get => movie; 
            set { SetProperty<Movie>(ref movie,value); }
        }

        private void GoBack()
        {
            Navigation.GoBack();
        }
    }
}
