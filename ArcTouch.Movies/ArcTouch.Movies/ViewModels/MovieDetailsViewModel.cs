using ArcTouch.Movies.Helpers.Communication;
using ArcTouch.Movies.Helpers.Navigation;
using ArcTouch.Movies.Models;
using ArcTouch.Movies.Models.Messages;
using ArcTouch.Movies.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcTouch.Movies.ViewModels
{
    public class MovieDetailsViewModel : ViewModelBase
    {
        IMoviesRepository moviesRepository;
        IRequestHelper requestHelper;

        public MovieDetailsViewModel(INavigationService navigation, Func<string, string, string, Task> displayAlertFunc,
            IMoviesRepository moviesRepository,IRequestHelper requestHelper): base(navigation,displayAlertFunc) 
        {
            this.moviesRepository = moviesRepository;
            this.requestHelper = requestHelper;
        }

        private Movie movie;
        public Movie Movie
        {
            get => movie; 
            set { SetProperty<Movie>(ref movie,value); }
        }

        private string genres;
        public string Genres
        {
            get => genres;
            set { SetProperty<string>(ref genres, value); }
        }

        private Uri movieImage;
        public Uri MovieImage {
            get => movieImage; 
            set { SetProperty<Uri>(ref movieImage, value); }
        }

        internal async Task InitAsync(Movie movie)
        {
            Movie = movie;
           // MovieImage = new Uri(config.GetValue<string>("ImageBaseUrl") + config.GetValue<string>("PosterSize") + Movie.PosterPath);

            var allGenres = await GetGenresAsync();
            var genresList = allGenres.Where(g => Movie.GenreIds.Any(gid => gid == g.Id)).Select(g => g.Name).ToList();
           
            StringBuilder genresBdr = new StringBuilder();
            for (int i = 0; i < genresList.Count(); i++)
            {
                genresBdr.Append(genresList[i]);
                if (i < genresList.Count - 1)
                    genresBdr.Append(", ");
            }

            Genres = genresBdr.ToString();

        }

        private async Task<List<Genre>> GetGenresAsync()
        {
            if (await moviesRepository.GetGenresCountAsync() > 0)
            {
                return await moviesRepository.ListGenresAsync();
            }
            else
            {
                try
                {
                    var genres = await requestHelper.GetAsync<GetGenresResponse>(string.Format(Resources.GetGenresUrl,Resources.ApiKey));
                    await moviesRepository.AddGenresAsync(genres.Genres);
                    return genres.Genres;

                }
                catch (Exception ex)
                {
                    await DisplayAlertAsync("Alert",ex.Message,"Ok");
                    await Navigation.GoBack();
                    return null;
                }

            }

        }
    }
}
