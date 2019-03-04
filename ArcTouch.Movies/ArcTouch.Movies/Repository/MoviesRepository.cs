using ArcTouch.Movies.Models;
using ArcTouch.Movies.Repository;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(MoviesRepository))]
namespace ArcTouch.Movies.Repository
{
    public class MoviesRepository : IMoviesRepository
    {
        SQLiteAsyncConnection database;
        public MoviesRepository()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "moviesdb.db3");

            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Genre>();
        }

        public async Task<int> GetGenresCountAsync()
        {
            return await database.Table<Genre>().CountAsync();
        }

        public async Task AddGenresAsync(List<Genre> genres)
        {
            await database.InsertAllAsync(genres);
        }

        public async Task<List<Genre>> ListGenresAsync()
        {
            return await database.Table<Genre>().ToListAsync();
        }
    }
}
