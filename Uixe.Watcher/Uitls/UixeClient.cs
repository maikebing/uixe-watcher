using MonkeyCache.LiteDB;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Uitls
{
    public class UixeClient
    {
        public T GetDataBy<T>(string _key,string ip, string api, object objparam = null)
        {
            return GetCatchOrCreate(_key, () =>
            {
                var result1 = default(T);
                var client = Create(ip,api);
                var request = new RestRequest(Method.GET);
                if (objparam != null)
                {
                    objparam.GetType().GetProperties().ToList().ForEach(p =>
                    {
                        request.AddParameter(p.Name, p.GetValue(objparam));
                    });
                }
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<T>>(response.Content);
                    if (result.code == 200)
                    {
                        result1 = result.data;
                    }
                }
                return result1;
            });
        }

        public List<ProvCode> GetProvCodes(string ip) => GetDataBy<List<ProvCode>>( "ProvCodes",ip, "/Plazas/ProvCodes");

        public List<ProvPlazaInfo> GetProvPlazaInfo(string ip, string ProvId) => GetDataBy<List<ProvPlazaInfo>>($"ProvCodes_{ProvId}",ip, "/Plazas/ProvPlazaInfo", new { ProvId });

        public string GetProvByPlaza(string ip,string plazaid) => GetDataBy<string>($"ProvByPlaza_{plazaid}",ip, "/Plazas/ProvByPlaza", new { plazaid });

        private T GetCatchOrCreate<T>(string _key, Func<T> fc)
        {
            if (!Barrel.Current.Exists(_key) || Barrel.Current.IsExpired(_key))
            {
                Barrel.Current.Add(_key, fc.Invoke(), TimeSpan.FromDays(7));
            }
            return Barrel.Current.Get<T>(_key);
        }

        private RestClient Create(string ip,string api)
        {
            var client = new RestClient($"http://{ip}:8080/api{api}");
            client.Timeout = -1;
            return client;
        }
    }
}