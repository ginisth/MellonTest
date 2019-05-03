using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class GetToken
    {
        public string Token { get; set; }

        public async Task<string> CatchToken(string username, string password)
        {
            // Prevent SSL exceptions
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);

            client.BaseAddress = new Uri("https://34.240.190.150/api/api-token-auth/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var body = new List<KeyValuePair<string, string>>
            {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
            };

            var content = new FormUrlEncodedContent(body);

            try
            {
                HttpResponseMessage response = await
                    client.PostAsync("https://34.240.190.150/api/api-token-auth/", content);

                response.EnsureSuccessStatusCode();
                string token = null;
                if (response.IsSuccessStatusCode)
                    token = await response.Content.ReadAsStringAsync();

                GetToken desirializedToken = Newtonsoft.Json.JsonConvert.DeserializeObject<GetToken>(token);

                return desirializedToken.Token;
            }
            catch (HttpRequestException e)
            {
                return e.Message.ToString();
            }
        }
    }
}
