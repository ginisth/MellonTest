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


        public async Task<Director> GetDirectorAsync(int id)
        {
            string director = null;
            HttpResponseMessage response = await Client.GetAsync("directors//" + id + "//");
            if (response.IsSuccessStatusCode)
            {
                director = await response.Content.ReadAsStringAsync();
            }

            if (director != null)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Director>(director);
            else
                return null;
        }


        public async Task<HttpStatusCode> CreateDirectorAsync(Director director)
        {
            Director newDirector = new Director()
            {
                name = director.name,
                birthday = director.birthday
            };

            try
            {
                HttpResponseMessage response = await
                    Client.PostAsJsonAsync("directors//", newDirector);
                response.EnsureSuccessStatusCode();

                return response.StatusCode;
            }
            catch (HttpRequestException)
            {
                return HttpStatusCode.BadRequest;
            }
        }


        public async Task<HttpStatusCode> UpdateDirectorAsync(Director director)
        {
            try
            {
                HttpResponseMessage response = await Client.PutAsJsonAsync(
                    "directors//" + director.id + "//", director);
                response.EnsureSuccessStatusCode();

                return response.StatusCode;
            }
            catch (HttpRequestException)
            {
                return HttpStatusCode.BadRequest;
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
