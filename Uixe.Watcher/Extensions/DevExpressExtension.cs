using DevExpress.LookAndFeel;
using Microsoft.AspNetCore.Builder;

namespace Uixe.Watcher.Extensions
{
    public static class DevExpressExtension
    {

        public static void UseDevExpress(this IApplicationBuilder app)
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
           // UserLookAndFeel.Default.SetSkinStyle(_runtimeSetting.SkinStyle);
        }
  
    }
}
