using ArcTouch.Movies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArcTouch.Movies.Repository
{
    public interface IMoviesRepository
    {
        Task<int> GetGenresCountAsync();
        
        Task AddGenresAsync(List<Genre> genres);

        Task<List<Genre>> ListGenresAsync();
    }
}