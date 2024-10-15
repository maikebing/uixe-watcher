using DevExpress.XtraSpellChecker.Parser;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Uitls
{
    public  static class LaneExtension
    {

        public static RestClient  CreateClient(this T_Lane lane)
        {
            var  client = new RestClient( new RestClientOptions($"http://{lane.Ip}:10000/") {  FollowRedirects=false , Timeout=TimeSpan.FromSeconds(10)});
            client.AddDefaultHeader(KnownHeaders.Accept, "*/*");
            client.AddDefaultHeader(KnownHeaders.ContentType, "application/json");
            return client;
        }
        public static async  Task<bool> Ping(this T_Lane lane)
        {
            var p = new Ping();
            var ping = await p.SendPingAsync(lane.Ip);
            return ping.Status== IPStatus.Success;
        }
        public static async Task<ApiResult> SendMsg<T>(this T_Lane lane, string path, T msg)
        {
            var client = lane.CreateClient();
            var request = new RestRequest($"/api/{path}", Method.Post);
            request.AddBody(Newtonsoft.Json.JsonConvert.SerializeObject(msg, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() }), "application/json");
            var response = await client.PostAsync(request);
            var apiResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult>(response.Content);
            return apiResult;
        }

        public static async Task<ApiResult> TCO_Confirm(this T_Lane lane, MSG_TCOConfirm confirm)
        {
            using (var client = lane.CreateClient())
            {
                var request = new RestRequest("/api/tco/confirm/", Method.Post);
                request.AddBody(Newtonsoft.Json.JsonConvert.SerializeObject(confirm, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() }), "application/json");
                var response = await client.PostAsync(request);
                var apiResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult>(response.Content);
                client.Dispose();
                return apiResult;
            }
        }
 
    }
}