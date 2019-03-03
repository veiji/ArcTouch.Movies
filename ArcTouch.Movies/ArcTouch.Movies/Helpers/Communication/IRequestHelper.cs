using System.Threading.Tasks;

namespace ArcTouch.Movies.Helpers.Communication
{
    public interface IRequestHelper
    {
        Task<T> GetAsync<T>(string address);
    }
}