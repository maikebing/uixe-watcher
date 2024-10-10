using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher.Controls
{
    public partial class VNCViewer : DevExpress.XtraEditors.XtraUserControl
    {
        public VNCViewer()
        {
            InitializeComponent();
        }
        public string IPAddress { get; set; }
        public int Port { get; set; }   
        public string Password { get; set; }
        public string Caption { get; set; }


        private void VNCViewer_Load(object sender, EventArgs e)
        {
            try
            {
                var vnc = VNCUtils.Login(this, IPAddress, 5900, Password ?? "kissme");
                if (vnc != null)
                {
                    vnc.Text = Caption;
                    this.Text = vnc.Text;
                }
            }
            catch (Exception ex)
            {


            }
        }
    }
}
