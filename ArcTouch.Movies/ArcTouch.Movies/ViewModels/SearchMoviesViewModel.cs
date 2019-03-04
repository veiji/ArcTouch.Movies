using ArcTouch.Movies.Helpers.Communication;
using ArcTouch.Movies.Helpers.Navigation;
using ArcTouch.Movies.Models.Messages;
using ArcTouch.Movies.Properties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArcTouch.Movies.ViewModels
{
    public class SearchMoviesViewModel : ListMoviesViewModel
    {
        public SearchMoviesViewModel(INavigationService navigation, Func<string, string, string, Task> displayAlertFunc, IRequestHelper requestHelper, Dictionary<int, string> genreCache) : base(navigation, displayAlertFunc, requestHelper)
        {
            GenreCache = genreCache;
        }

        #region Fields
        string titleToSearch;
        #endregion

        #region Properties
        ICommand searchCommand;
        public ICommand SearchCommand => searchCommand ?? (searchCommand = new Command<string>(Search));
        #endregion

        #region Methods
        private void Search(string title)
        {
            CurrentPage = 1;
            titleToSearch = title;
            _ = LoadMoviesAsync();
        }

        protected override async Task LoadMoviesAsync()
        {
            try
            {
                IsBusy = true;

                var response = await Request.GetAsync<GetMoviesResponse>(string.Format(Configurations.GetSearchMoviesUrl, CurrentPage, Configurations.ApiKey, titleToSearch));

                foreach (var item in response.Results)
                    Movies.Add(FillMovie(item, GenreCache));
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

        #endregion
    }
}
