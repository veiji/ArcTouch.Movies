using ArcTouch.Movies.Helpers;
using ArcTouch.Movies.Helpers.Communication;
using ArcTouch.Movies.Helpers.Navigation;
using ArcTouch.Movies.Models;
using ArcTouch.Movies.Models.Messages;
using ArcTouch.Movies.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArcTouch.Movies.ViewModels
{
    public class MainViewModel : ListMoviesViewModel
    {
        #region Fields
        readonly int daysToUpdateConfig = 2;
        IMoviesRepository moviesRepository;
        #endregion

        #region Ctor
        public MainViewModel(INavigationService navigation, Func<string, string, string, Task> displayAlertFunc, 
            IRequestHelper requestHelper, IMoviesRepository moviesRepository) : base(navigation,displayAlertFunc,requestHelper)
        {
            IsBusy = true;
            this.moviesRepository = moviesRepository;
                                   
            _ = InitAsync();            
        }
        #endregion

        #region Properties
        ICommand searchCommand;
        public ICommand SearchCommand => searchCommand ?? (searchCommand = new Command(NavigateToSearch));
        #endregion

        #region Methods
        private void NavigateToSearch()
        {
            Navigation.NavigateAsync(Constants.SEARCHPAGE, GenreCache, true);
        }

        private async Task InitAsync()
        {
            await Task.WhenAll(LoadConfigsAsync(), FillGenresCacheAsync(GenreCache));
            await LoadMoviesAsync();
            IsBusy = false;
        }

        protected override async Task LoadMoviesAsync()
        {
            try
            {
                IsBusy = true;

                var response = await Request.GetAsync<GetMoviesResponse>(string.Format(Resources.GetUpcomingMoviesUrl, CurrentPage,Resources.ApiKey));

                foreach (var item in response.Results)
                    Movies.Add(FillMovie(item,GenreCache));
                CurrentPage++;
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
                    var genres = await Request.GetAsync<GetGenresResponse>(string.Format(Resources.GetGenresUrl, Resources.ApiKey));
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
            if (lastDtSettingsUpdate.Date < DateTime.Now.Subtract(new TimeSpan(daysToUpdateConfig,0,0,0)).Date)
            {
                try
                {
                    var imageConfig = await Request.GetAsync<GetConfigResponse>(string.Format(Resources.GetConfigurationsUrl, Resources.ApiKey));
                    Preferences.Set(Constants.IMAGEBASEURL, imageConfig.Images.BaseUrl);
                    Preferences.Set(Constants.LISTPOSTERSIZE, imageConfig.Images.PosterSizes[3]);
                    Preferences.Set(Constants.BACKDROPSIZE, imageConfig.Images.BackdropSizes[2]);
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
