using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using MonkeyCache.LiteDB;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Uixe.Watcher
{
    public static class Program
    {
        // Fields
        public static frmMain MainForm;

        [STAThread]
        private static void Main(string[] args)
        {
            Barrel.ApplicationId = Assembly.GetExecutingAssembly().GetName().Name;
            Barrel.EncryptionKey = "future";
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.SkinStyle);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new frmMain();
            Application.Run(MainForm);
        }
    }
}