using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace Uixe.Watcher
{
    public static class AppUtils
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

    

       
    }
}