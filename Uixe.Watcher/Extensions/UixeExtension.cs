using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using System.Windows.Forms;

namespace Uixe.Watcher.Extensions
{
    public static class UixeExtension
    {
        public static void Invoke(this Control ctl, Action action)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke((MethodInvoker)delegate
                {
                    action.Invoke();
                });
            }
            else
            {
                action.Invoke();
            }
        }

        public static void Invoke(this Form ctl, Action action)
        {
            ctl.Invoke((MethodInvoker)delegate
            {
                action.Invoke();
            });
        }

        public static T Clone<T>(this T source) where T : class
        {
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(source);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr);
        }
        public static IWebHostBuilder ConfigureUserConfiguration(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"appsettings.User.json"), true, true);
            });
        }

    }
}