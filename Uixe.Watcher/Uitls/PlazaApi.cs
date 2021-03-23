using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Uitls
{
    public partial class PlazaApi
    {

        RestClient client;
        public PlazaApi(string serverapi)
        {
            client = new RestClient($"http://{serverapi}/rpt/api");
            client.Timeout = -1;
            client.FollowRedirects = false;
        }
        public RptLoginResult SysLogin(string username, string password, string stationId, string shortId)
        {
            RptLoginResult result = new RptLoginResult();
            var request = new RestRequest("/sys/login", Method.POST, DataFormat.Json);
            request.AddHeader("Content-Type", "application/json");
            // "{\r\n    \"username\": \"admin\",\r\n    \"password\": \"Pm7EArc4xPuiXjXR4zO9STYqTxnKFjCqjblBCTjngZFoxCvSbaQlrYaqgkmabWFl73c/wcHVXrQGwmiftmWdfe79ihK2NukgGrNbGu9D3yls2RfzXm+YndoQQUBEAfNpgvVn1pOfQQp0vG6z6gGq6GzKm0bUS7oKlxv0YjOtqOA=\",\r\n    \"stationId\": \"G0007650050090\",\r\n    \"shortId\": \"6500124\"\r\n}"
            request.AddJsonBody(new { username, password = RSAHelper.EncryptJava(Properties.Resources.RAS_PUBLIC_KEY, password), stationId, shortId });
            IRestResponse response = client.Execute(request);
            result = Newtonsoft.Json.JsonConvert.DeserializeObject<RptLoginResult>(response.Content);
            return result;
        }
        public ApiResult<UserRole[]> getRoleByUser(string username)
        {
            var request = new RestRequest($"sys/user/getRoleByUser?userName={username}", Method.POST, DataFormat.Json);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("token",  RuntimeSetting.Token?.token);
            IRestResponse response = client.Execute(request);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<UserRole[]>>(response.Content);
            return result;
        }
        public void Logout()
        {

        }
    }
}
