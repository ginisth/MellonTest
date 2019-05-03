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

        public async Task<string> GetMovieAsync(int id)
        {
            string movie = null;
            HttpResponseMessage response = await Client.GetAsync("movies//" + id + "//");
            if (response.IsSuccessStatusCode)
            {
                movie = await response.Content.ReadAsStringAsync();
            }

            if (movie != null)
            {
                Movie desirializedMovie = Newtonsoft.Json.JsonConvert.DeserializeObject<Movie>(movie);
                return desirializedMovie.id + " " + desirializedMovie.name + " " + desirializedMovie.year +
                    " " + desirializedMovie.director + " " + desirializedMovie.actors[0];
            }

            return "Bad request";
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

            HttpResponseMessage response = await
                Client.PostAsJsonAsync("movies//", newMovie);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }


        public async Task<string> UpdateMovieAsync(Movie movie)
        {
            try
            {
                HttpResponseMessage response = await Client.PutAsJsonAsync(
                    "movies//" + movie.id + "//", movie);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    return $"Actor {movie.name} updated";
                }

                return "Bad request";

            }
            catch (HttpRequestException e)
            {
                return e.Message.ToString();
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
