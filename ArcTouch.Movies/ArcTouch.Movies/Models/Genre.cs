
using SQLite;

namespace ArcTouch.Movies.Models
{
    public class Genre
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
