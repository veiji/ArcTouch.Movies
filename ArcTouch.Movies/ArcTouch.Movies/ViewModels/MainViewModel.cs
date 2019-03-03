using ArcTouch.Movies.Helpers.Communication;
using ArcTouch.Movies.Helpers.Navigation;
using ArcTouch.Movies.Models;
using ArcTouch.Movies.Models.Messages;
using ArcTouch.Movies.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using ArcTouch.Movies.Helpers;

namespace ArcTouch.Movies.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        readonly int daysToUpdateConfig = 2;
        int currentPage = 1;
        IRequestHelper requestHelper;
        IMoviesRepository moviesRepository;
        readonly Dictionary<int, string> genreCache = new Dictionary<int, string>();

        #endregion

        #region Properties
        private ObservableCollection<Movie> movies=new ObservableCollection<Movie>();
        public ObservableCollection<Movie> Movies
        {
            get => movies;
            set{SetProperty(ref movies, value);}
        } 
        
        public ICommand LoadCommand { get; set; }
        public ICommand NavigateToMovieCommand { get; set; }
        #endregion

        #region Ctor
        public MainViewModel(INavigationService navigation, Func<string, string, string, Task> displayAlertFunc, 
            IRequestHelper requestHelper, IMoviesRepository moviesRepository) : base(navigation,displayAlertFunc)
        {
            IsBusy = true;
            this.requestHelper = requestHelper;
            this.moviesRepository = moviesRepository;

            LoadCommand = new Command(() => _ = LoadMoviesAsync());
            
            NavigateToMovieCommand = new Command<Movie>((movie) => _ = NavigateToMovie(movie));
            _ = InitAsync();
            
        }
        #endregion

        #region Methods
        private async Task InitAsync()
        {
            await Task.WhenAll(LoadConfigsAsync(), FillGenresCacheAsync(genreCache));
            await LoadMoviesAsync();
            IsBusy = false;
        }

        private async Task LoadMoviesAsync()
        {
            try
            {
                IsBusy = true;

                var response = await requestHelper.GetAsync<GetUpcomingResponse>(string.Format(Resources.GetUpcomingMoviesUrl, currentPage,Resources.ApiKey));

                foreach (var item in response.Results)
                    Movies.Add(FillMovie(item,genreCache));
                currentPage++;
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync(Resources.Alert, ex.Message, Resources.Ok);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Movie FillMovie(Movie movie, Dictionary<int, string> genres)
        {
            int genreLenght = movie.GenreIds.Length;
            string[] genreList = new string[genreLenght];
            for (int i = 0; i < genreLenght; i++)            
                genreList[i] = genres[movie.GenreIds[i]];            

            movie.Genre = string.Join(", ", genreList);
            movie.FullBackdropPath = string.Concat(Preferences.Get(Constants.IMAGEBASEURL,""), Preferences.Get(Constants.BACKDROPSIZE,""),movie.BackdropPath);
            movie.FullPosterPath = string.Concat(Preferences.Get(Constants.IMAGEBASEURL, ""), Preferences.Get(Constants.LISTPOSTERSIZE, ""), movie.PosterPath);

            return movie;
        }

        private async Task NavigateToMovie(Movie movie)
        {
            await Navigation.NavigateAsync(Constants.DETAILSPAGE,movie,true);                        
        }

        private async Task<List<Genre>> GetGenresAsync()
        {
            //TODO: These genres stored in DB, someday could be outdated.
            try
            {
                if (await moviesRepository.GetGenresCountAsync() > 0)
                {
                    return await moviesRepository.ListGenresAsync();
                }
                else
                {
                    var genres = await requestHelper.GetAsync<GetGenresResponse>(string.Format(Resources.GetGenresUrl, Resources.ApiKey));
                    await moviesRepository.AddGenresAsync(genres.Genres);
                    return genres.Genres;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync(Resources.Alert, ex.Message, Resources.Ok);
                return null;
            }
        }

        private async Task LoadConfigsAsync()
        {
            var lastDtSettingsUpdate = Preferences.Get(Constants.LASTDATESETTINGSUPDATED, DateTime.MinValue);
            if (lastDtSettingsUpdate.Date < DateTime.Now.Date)
            {
                try
                {
                    var imageConfig = await requestHelper.GetAsync<GetConfigResponse>(string.Format(Resources.GetConfigurationsUrl, Resources.ApiKey));
                    Preferences.Set(Constants.IMAGEBASEURL, imageConfig.Images.BaseUrl);
                    Preferences.Set(Constants.LISTPOSTERSIZE, imageConfig.Images.PosterSizes[3]);
                }
                catch (Exception ex)
                {
                    await DisplayAlertAsync(Resources.Alert, ex.Message, Resources.Ok);
                }

                Preferences.Set(Constants.LASTDATESETTINGSUPDATED, DateTime.Now);
            }
        }
    
        private async Task FillGenresCacheAsync(Dictionary<int, string> cache)
        {
            try
            {
                List<Genre> genres = await GetGenresAsync();

                foreach (var genre in genres)
                {
                    if (!cache.ContainsKey(genre.Id))
                        cache.Add(genre.Id, genre.Name);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync(Resources.Alert, ex.Message, Resources.Ok);                
            }
        }
        #endregion
    }
}
