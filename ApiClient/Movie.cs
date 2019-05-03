using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class Movie
    {
        public int id { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public int director { get; set; }
        public List<int> actors { get; set; }
        public HttpClient Client { get; set; }


        public Movie()
        {

        }

        public Movie(string token)
        {

            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://34.240.190.150/api/");
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Token " + token);
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            string movies = null;
            HttpResponseMessage response = await Client.GetAsync("movies//");
            if (response.IsSuccessStatusCode)
            {
                movies = await response.Content.ReadAsStringAsync();
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Movie>>(movies);
        }

        public async Task<Movie> GetMovieAsync(int id)
        {
            string movie = null;
            HttpResponseMessage response = await Client.GetAsync("movies//" + id + "//");
            if (response.IsSuccessStatusCode)
            {
                movie = await response.Content.ReadAsStringAsync();
            }

            if (movie != null)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Movie>(movie);
            else
                return null;
        }

        public async Task<HttpStatusCode> CreateMovieAsync(Movie movie)
        {
            Movie newMovie = new Movie()
            {
                name = movie.name,
                year = movie.year,
                director=movie.director,
                actors=movie.actors
            };

            try
            {
                HttpResponseMessage response = await
                    Client.PostAsJsonAsync("movies//", newMovie);
                response.EnsureSuccessStatusCode();

                return response.StatusCode;
            }
            catch(HttpRequestException)
            {
               return HttpStatusCode.BadRequest;
            }
        }


        public async Task<HttpStatusCode> UpdateMovieAsync(Movie movie)
        {
            try
            {
                HttpResponseMessage response = await Client.PutAsJsonAsync(
                    "movies//" + movie.id + "//", movie);
                response.EnsureSuccessStatusCode();
                
                    return response.StatusCode;
            }
            catch (HttpRequestException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<HttpStatusCode> DeleteMovieAsync(int id)
        {
            HttpResponseMessage response = await Client.DeleteAsync(
                "movies//" + id + "//");
            return response.StatusCode;
        }
    }
}
