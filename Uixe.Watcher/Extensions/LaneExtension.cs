using Newtonsoft.Json.Serialization;
using RestSharp;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Uitls
{
    public  static class LaneExtension
    {

        public static RestClient  CreateClient(this Lane lane)
        {
            var  client = new RestClient( new RestClientOptions($"http://{lane.ip}:10000/") { Timeout=5, FollowRedirects=false });
            client.AddDefaultHeader(KnownHeaders.Accept, "*/*");
            client.AddDefaultHeader(KnownHeaders.ContentType, "application/json");
            return client;
        }
        public static async Task<ApiResult> SendMsg<T>(this Lane lane,string path, T msg)
        {
            ApiResult apiResult = new ApiResult();
            try
            {
                var client = lane.CreateClient();
                var request = new RestRequest("/api/tco/confirm/", Method.Post);
                request.AddBody(Newtonsoft.Json.JsonConvert.SerializeObject(msg, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() }), "application/json");
                var response = await client.PostAsync(request);
                apiResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult>(response.Content);
            }
            catch (System.Exception ex)
            {


            }
            return apiResult;
        }
    
        public static async Task<ApiResult> TCO_Confirm(this Lane lane, MSG_TCOConfirm confirm)
        {
            ApiResult apiResult =  new ApiResult();
            try
            {
                var client = lane.CreateClient();
                var request = new RestRequest("/api/tco/confirm/", Method.Post);
                request.AddBody(Newtonsoft.Json.JsonConvert.SerializeObject(confirm, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver=new DefaultContractResolver()  }), "application/json");
                var response = await client.PostAsync(request);
                apiResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult>(response.Content);
            }
            catch (System.Exception ex)
            {

               
            }
            return apiResult;
        }
       
    }
}