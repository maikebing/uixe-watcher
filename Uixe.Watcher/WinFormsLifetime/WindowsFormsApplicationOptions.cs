using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Uixe.Watcher
{
    public class WindowsFormsApplicationOptions
    {
#if NETCOREAPP

        public HighDpiMode HighDpiMode { get; set; }
#endif
        public bool EnableVisualStyles { get; set; }
        public bool CompatibleTextRenderingDefault { get; set; }

        public WindowsFormsApplicationOptions()
        {
#if NETCOREAPP
            HighDpiMode = HighDpiMode.SystemAware;
#endif
            EnableVisualStyles = true;
            CompatibleTextRenderingDefault = false;
        }
    }
}
