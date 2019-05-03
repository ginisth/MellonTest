using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class Actors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public HttpClient Client { get; set; }

        public Actors(string token)
        {
            
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://34.240.190.150/api/actors/");
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Token " + token);
        }


        public async Task<List<Actors>> GetActors()
        {
            string actors = null;
            HttpResponseMessage response = await Client.GetAsync("https://34.240.190.150/api/actors/");
            if (response.IsSuccessStatusCode)
            {
                actors = await response.Content.ReadAsStringAsync();
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Actors>>(actors);
        }
    }
}
