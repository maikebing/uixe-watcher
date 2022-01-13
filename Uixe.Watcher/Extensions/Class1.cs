using DevExpress.LookAndFeel;
using Microsoft.AspNetCore.Builder;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uixe.Watcher.Extensions
{

    public static class DebugExtensions
    {

        public static   void UseDebugLane(this IApplicationBuilder app,string laneipaddress,string tcoipaddress)
        {
            try
            {
                var client = new RestClient($"http://{laneipaddress}:10000");
                var request = new RestRequest("/api/callback/tco", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(new { ipAddress = tcoipaddress });
                client.ExecutePostAsync(request);
            }
            catch (Exception)
            {

          
            }
        }
   
    }
    public static class DevExpressExtension
    {

        public static void UseDevExpress(this IApplicationBuilder app)
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.SkinStyle);
        }
  
    }
}
