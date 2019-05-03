using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class Director
    {
        public int id { get; set; }
        public string name { get; set; }

        // using string variable on birthday cause REST API allows only (yyyy-MM-dd) format
        // Datetime variable can't reach that kind of format
        public string birthday { get; set; }
        public HttpClient Client { get; set; }

        public Director()
        {

        }

        public Director(string token)
        {

            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://34.240.190.150/api/");
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Token " + token);
        }


        public async Task<List<Director>> GetDirectorsAsync()
        {
            string directors = null;
            HttpResponseMessage response = await Client.GetAsync("directors//");
            if (response.IsSuccessStatusCode)
            {
                directors = await response.Content.ReadAsStringAsync();
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Director>>(directors);
        }


        public async Task<string> GetDirectorAsync(int id)
        {
            string director = null;
            HttpResponseMessage response = await Client.GetAsync("directors//" + id + "//");
            if (response.IsSuccessStatusCode)
            {
                director = await response.Content.ReadAsStringAsync();
            }

            if (director != null)
            {
                Director desirializedDirector = Newtonsoft.Json.JsonConvert.DeserializeObject<Director>(director);
                return desirializedDirector.id + " " + desirializedDirector.name + " " + desirializedDirector.birthday;
            }

            return "Bad request";
        }


        public async Task<HttpStatusCode> CreateDirectorAsync(Director director)
        {
            Director newDirector = new Director()
            {
                name = director.name,
                birthday = director.birthday
            };

            HttpResponseMessage response = await
                Client.PostAsJsonAsync("directors//", newDirector);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }


        public async Task<string> UpdateDirectorAsync(Director director)
        {
            try
            {
                HttpResponseMessage response = await Client.PutAsJsonAsync(
                    "directors//" + director.id + "//", director);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    return $"Actor {director.name} updated";
                }

                return "Bad request";

            }
            catch (HttpRequestException e)
            {
                return e.Message.ToString();
            }
        }


        public async Task<HttpStatusCode> DeleteDirectorAsync(int id)
        {
            HttpResponseMessage response = await Client.DeleteAsync(
                "directors//" + id + "//");
            return response.StatusCode;
        }
    }
}
