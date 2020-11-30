using MonkeyCache.LiteDB;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Uitls
{
    public static class TollInfo
    {
        public static Plaza GetTollInfo()
        {
            return GetTollInfo(Properties.Settings.Default.plazaid);
        }
        public static Plaza GetTollInfo(string toll_id)
        {
              string _key = "get_toll_info"+toll_id;
            if (!Barrel.Current.Exists(_key) || Barrel.Current.IsExpired(_key))
            {
                var client = new RestClient("http://10.165.70.45:5000/get_toll_info");
                client.Timeout = -1;
                client.FollowRedirects = false;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", $"{{\r\n    \"toll_id\": \"{toll_id}\"\r\n}}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Debug.WriteLine(response.Content);
                var plaza= Newtonsoft.Json.JsonConvert.DeserializeObject<Plaza>(response.Content);
                Barrel.Current.Add(_key, plaza, TimeSpan.FromDays(7));
            }
            return Barrel.Current.Get<Plaza>(_key);

        }
    }
}
