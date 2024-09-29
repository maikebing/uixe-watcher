using RestSharp;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;
using WhoIamDtos;

namespace Uixe.Watcher.Uitls
{
    public static class TollInfo
    {
#if DEBUG
        private const string _baseUrl = "http://127.0.0.1:5137/";
#else
        private const string _baseUrl = "http://10.165.84.44/";
#endif

        public static async Task<whoiam> Guesswhoiam()
        {
            whoiam whoiam = null;
            var client = new RestClient("http://10.165.70.45:5000/").AddDefaultHeader(KnownHeaders.Accept, "*/*");
            var request = new RestRequest("/guesswhoiam", Method.Post).AddHeader("Content-Type", "application/json");
            var response = await client.ExecutePostAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Debug.WriteLine(response.Content);
                whoiam = Newtonsoft.Json.JsonConvert.DeserializeObject<WhoIamDtos.whoiam>(response.Content);
                await upload_boss(response.Content);
            }
            return whoiam;
        }

        public static async Task<ApiResult<T_Boss>> GuessMyInfo()
        {
            ApiResult<T_Boss> whoiam=null;
              var client = new RestClient(_baseUrl).AddDefaultHeader(KnownHeaders.Accept, "*/*");
            var request = new RestRequest("/guesswhoiam", Method.Post).AddHeader("Content-Type", "application/json");
            var response = await client.ExecutePostAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Debug.WriteLine(response.Content);
                whoiam = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult< T_Boss>>(response.Content);
            }
            return whoiam;
        }

        public static async Task upload_boss(string json)
        {
            try
            {
                using var client = new RestClient(_baseUrl).AddDefaultHeader(KnownHeaders.Accept, "*/*");
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
