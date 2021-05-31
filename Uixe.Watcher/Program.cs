using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using MonkeyCache;
using MonkeyCache.LiteDB;
using MQTTnet;
using MQTTnet.Server;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Extensions;

namespace Uixe.Watcher
{
    public static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        private enum DpiAwareness
        {
            None = 0,
            SystemAware = 1,
            PerMonitorAware = 2
        }
        [DllImport("Shcore.dll")]
        static extern int SetProcessDpiAwareness(int PROCESS_DPI_AWARENESS);
        // Fields
        public static frmMain MainForm;


        public static bool mqttserver { get; set; }
        [STAThread]
        private static void Main(string[] args)
        {
            AppExtension.RunOnlyOneInstance(() =>
            {
#if NETCOREAPP
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
#else
                // SetProcessDpiAwareness((int)DpiAwareness.SystemAware);
#endif
                args.ToList().ForEach(s =>
                        {
                            if (s == "--mqttserver")
                            {
                                mqttserver = true;
                            }
                        });
                if (mqttserver)
                {
                    Task.Run(async () =>
                    {
                        var mqttServer = new MqttFactory().CreateMqttServer();
                        var opt = new MqttServerOptions();
                        opt.DefaultEndpointOptions.BoundInterNetworkAddress = IPAddress.Any;
                        mqttServer.UseApplicationMessageReceivedHandler(c => Debug.WriteLine($"{ c.ClientId} {c.ApplicationMessage.Topic}"));
                        await mqttServer.StartAsync(opt);
                    });
                }
                Application.ThreadException += Application_ThreadException;
                try
                {
                    Barrel.ApplicationId = Assembly.GetExecutingAssembly().GetName().Name;
                    Barrel.EncryptionKey = "future";
                    BarrelUtils.SetBaseCachePath(AppContext.BaseDirectory);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.InnerException?.Message);

                }
                try
                {
                    DevExpress.Skins.SkinManager.EnableFormSkins();
                    DevExpress.UserSkins.BonusSkins.Register();
                    UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.SkinStyle);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + ex.InnerException?.Message);
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                MainForm = new frmMain();
                Application.Run(MainForm);

            });
        }
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message + e.Exception.InnerException?.Message);
        }
    }
}