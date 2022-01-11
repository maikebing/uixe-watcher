using RestSharp;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Uitls
{
    public partial class PlazaApi
    {
        private RestClient client;

        public PlazaApi(string serverapi)
        {
            client = new RestClient( new RestClientOptions($"http://{serverapi}/") { Timeout=-1, FollowRedirects=false });
            client.AddDefaultHeader(KnownHeaders.Accept, "*/*");
        }

        public async Task<RptLoginResult> SysLogin(string username, string password, string stationId, string shortId)
        {
            RptLoginResult result = new RptLoginResult();
            var request = new RestRequest("/rpt/api/sys/login", Method.Post );
            request.AddHeader("Content-Type", "application/json");
            // "{\r\n    \"username\": \"admin\",\r\n    \"password\": \"Pm7EArc4xPuiXjXR4zO9STYqTxnKFjCqjblBCTjngZFoxCvSbaQlrYaqgkmabWFl73c/wcHVXrQGwmiftmWdfe79ihK2NukgGrNbGu9D3yls2RfzXm+YndoQQUBEAfNpgvVn1pOfQQp0vG6z6gGq6GzKm0bUS7oKlxv0YjOtqOA=\",\r\n    \"stationId\": \"G0007650050090\",\r\n    \"shortId\": \"6500124\"\r\n}"
            var body=   new { username, password = RSAHelper.EncryptJava(Properties.Resources.RAS_PUBLIC_KEY, password), stationId, shortId };
            request.AddJsonBody( body);
            var response = await client.PostAsync(request);
            result = Newtonsoft.Json.JsonConvert.DeserializeObject<RptLoginResult>(response.Content);
            return result;
        }

        public async Task< ApiResult<UserRole[]>> getRoleByUser(string username,string token)
        {
            var request = new RestRequest($"/rpt/api/sys/user/getRoleByUser?userName={username}", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("token",token);
             var response =  await client.ExecutePostAsync(request);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<UserRole[]>>(response.Content);
            return result;
        }

        public void Logout()
        {
        }
    }
}