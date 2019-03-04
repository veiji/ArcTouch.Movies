using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ArcTouch.Movies.Helpers;
using ArcTouch.Movies.Helpers.Communication;
using ArcTouch.Movies.Helpers.Navigation;
using ArcTouch.Movies.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArcTouch.Movies.ViewModels
{
    public abstract class ListMoviesViewModel : ViewModelBase
    {
        public ListMoviesViewModel(INavigationService navigation, Func<string, string, string, Task> displayAlertFunc,IRequestHelper requestHelper) : base(navigation, displayAlertFunc)
        {
            Request = requestHelper;
        }

        #region Properties
        protected IRequestHelper Request { get; set; }
        protected int CurrentPage { get; set; } = 1;
        protected Dictionary<int, string> GenreCache { get; set; } = new Dictionary<int, string>();

        private ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> Movies
        {
            get => movies;
            set { SetProperty(ref movies, value); }
        }

        ICommand loadCommand;
        public ICommand LoadCommand => loadCommand ?? (loadCommand = new Command(() => _ = LoadMoviesAsync()));

        ICommand navigateToMovieCommand;
        public ICommand NavigateToMovieCommand => navigateToMovieCommand ?? (navigateToMovieCommand = new Command<Movie>((movie) => _ = NavigateToMovie(movie)));
        #endregion

        #region Methods
        protected abstract Task LoadMoviesAsync();

        protected Movie FillMovie(Movie movie, Dictionary<int, string> genres)
        {
            int genreLenght = movie.GenreIds.Length;
            string[] genreList = new string[genreLenght];
            for (int i = 0; i < genreLenght; i++)
                genreList[i] = genres[movie.GenreIds[i]];

            movie.Genre = string.Join(", ", genreList);
            movie.FullBackdropPath = string.Concat(Preferences.Get(Constants.IMAGEBASEURL, ""), Preferences.Get(Constants.BACKDROPSIZE, ""), movie.BackdropPath);
            movie.FullPosterPath = string.Concat(Preferences.Get(Constants.IMAGEBASEURL, ""), Preferences.Get(Constants.LISTPOSTERSIZE, ""), movie.PosterPath);

            return movie;
        }

        private async Task NavigateToMovie(Movie movie)
        {
            await Navigation.NavigateAsync(Constants.DETAILSPAGE, movie, true);
        }
        #endregion
    }
}
