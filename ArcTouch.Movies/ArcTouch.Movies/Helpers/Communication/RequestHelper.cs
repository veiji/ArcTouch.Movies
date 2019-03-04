using ArcTouch.Movies.Helpers.Communication;
using ArcTouch.Movies.Helpers.Exceptions;
using ArcTouch.Movies.Models.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

[assembly: Dependency(typeof(RequestHelper))]
namespace ArcTouch.Movies.Helpers.Communication
{
    public class RequestHelper : IRequestHelper
    {
        public async Task<T> GetAsync<T>(string address)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new NetworkAvailabilityException("Please, check your internet connection and try again later.");
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(address))
                {
                    var settings = new JsonSerializerSettings { ContractResolver = new DefaultContractResolver{NamingStrategy = new SnakeCaseNamingStrategy()}};
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(), settings);
                    }
                    else
                    {
                        var error = JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync(), settings);
                        throw new Exception(error.StatusMessage??error.Errors.FirstOrDefault());
                    }
                }
            }
        }

    }
}
