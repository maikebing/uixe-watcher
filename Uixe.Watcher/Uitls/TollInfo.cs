using RestSharp;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Uitls
{
    public static class TollInfo
    {
        public static async Task<Plaza> GetTollInfo(string toll_id, bool reset = false)
        {
            Plaza plaza1 = null;
            var client = new RestClient("http://10.165.70.45:5000/").AddDefaultHeader(KnownHeaders.Accept, "*/*");
            var request = new RestRequest("/get_toll_info", Method.Post).AddHeader("Content-Type", "application/json").AddJsonBody(new { toll_id });
            var response = await client.ExecutePostAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Debug.WriteLine(response.Content);
                plaza1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Plaza>(response.Content);
            }
            return plaza1;
        }
        public static async Task<whoiam> Guesswhoiam()
        {
            whoiam whoiam = null;
             var client = new RestClient("http://10.165.70.45:5000/").AddDefaultHeader(KnownHeaders.Accept, "*/*");
            var request = new RestRequest("/guesswhoiam", Method.Post).AddHeader("Content-Type", "application/json");
            var response = await client.ExecutePostAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Debug.WriteLine(response.Content);
                whoiam = Newtonsoft.Json.JsonConvert.DeserializeObject<whoiam>(response.Content);
            }
            return whoiam;
        }
    }
}
