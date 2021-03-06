﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.Net;

namespace ApiClient
{
    public class Actor
    {
        public int id { get; set; }
        public string name { get; set; }

        // using string variable on birthday cause REST API allows only (yyyy-MM-dd) format
        // Datetime variable can't reach that kind of format
        public string birthday { get; set; }
        public HttpClient Client { get; set; }

        public Actor()
        {

        }

        public Actor(string token)
        {

            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://34.240.190.150/api/");
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Token " + token);
        }


        public async Task<List<Actor>> GetActorsAsync()
        {
            string actors = null;
            HttpResponseMessage response = await Client.GetAsync("actors//");
            if (response.IsSuccessStatusCode)
            {
                actors = await response.Content.ReadAsStringAsync();
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Actor>>(actors);
        }


        public async Task<Actor> GetActorAsync(int id)
        {
            string actor = null;
            HttpResponseMessage response = await Client.GetAsync("actors//" + id + "//");
            if (response.IsSuccessStatusCode)
            {
                actor = await response.Content.ReadAsStringAsync();
            }

            if (actor != null)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Actor>(actor);
            else
                return null;
        }


        public async Task<HttpStatusCode> CreateActorAsync(Actor actor)
        {
            Actor newActor = new Actor()
            {
                name = actor.name,
                birthday = actor.birthday
            };

            try
            {
                HttpResponseMessage response = await
                    Client.PostAsJsonAsync("actors//", newActor);
                response.EnsureSuccessStatusCode();

                return response.StatusCode;
            }
            catch (HttpRequestException)
            {
                return HttpStatusCode.BadRequest;
            }
        }


        public async Task<HttpStatusCode> UpdateActorAsync(Actor actor)
        {
            try
            {
                HttpResponseMessage response = await Client.PutAsJsonAsync(
                    "actors//" + actor.id + "//", actor);
                response.EnsureSuccessStatusCode();

                return response.StatusCode;
            }
            catch(HttpRequestException )
            {
                return HttpStatusCode.BadRequest;
            }
        }


        public async Task<HttpStatusCode> DeleteActorAsync(int id)
        {
            HttpResponseMessage response = await Client.DeleteAsync(
                "actors//" + id + "//");
            return response.StatusCode;
        }
    }
}
