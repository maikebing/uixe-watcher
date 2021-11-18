using DevExpress.LookAndFeel;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uixe.Watcher.Extensions
{
    public static class DevExpressExtension
    {

        public static void UseDevExpress(this IApplicationBuilder app)
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.SkinStyle);
        }
  
    }
}
