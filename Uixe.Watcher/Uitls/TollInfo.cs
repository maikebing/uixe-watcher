using RestSharp;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Uitls
{
    public static class TollInfo
    {

 
        private const string BaseUrl = "http://10.165.70.45:5000/";
     
        public static async Task<whoiam> Guesswhoiam()
        {
            whoiam whoiam = null;
            var client = new RestClient(BaseUrl).AddDefaultHeader(KnownHeaders.Accept, "*/*");
            var request = new RestRequest("/guesswhoiam", Method.Post).AddHeader("Content-Type", "application/json");
            var response = await client.ExecutePostAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Debug.WriteLine(response.Content);
                whoiam = Newtonsoft.Json.JsonConvert.DeserializeObject<whoiam>(response.Content);
                await upload_boss(response.Content);
            }
            return whoiam;
        }

        public static async Task upload_boss(string json)
        {
            try
            {
                using var client = new RestClient("http://10.165.84.44/").AddDefaultHeader(KnownHeaders.Accept, "*/*");
                var request = new RestRequest("/api/Bosses/upload_boss", Method.Post).AddHeader("Content-Type", "application/json");
                request.AddStringBody(json, DataFormat.Json);
                var response = await client.ExecutePostAsync(request);
                Debug.WriteLine(response.Content);
            }
            catch (Exception)
            {

      
            }
           
        }

    }
}
