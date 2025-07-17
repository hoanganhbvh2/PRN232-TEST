using givenAPI.Models;
using System.Net.Http.Json;

namespace Q2.Service
{
    public class ApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string? baseUrl = "";
        public ApiClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            baseUrl = configuration.GetValue<string>("GivenAPIBaseUrl");
            Console.WriteLine("Base URL: " + baseUrl);
        }

        public IEnumerable<Director> GetAllDirectors()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<Director>>(baseUrl + "/api/Directors/GetDirectors").Result;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<Movie>>(baseUrl + "/api/Movies/GetMovies").Result;
        }

        public IEnumerable<Movie> GetMoviesByDirectorId(int directorId)
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<Movie>>(baseUrl + $"/api/Movies/GetMoviesByDirectorId/{directorId}").Result;
        }
        public HttpResponseMessage DeleteMovie(int id)
        {
            
            return _httpClient.DeleteAsync(baseUrl + $"/api/Movies/DeleteMovie/{id}").Result;
        }
    }
}
