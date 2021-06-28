using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uixe.Watcher
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnOpenPlaza_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmPlaza plaza = new frmPlaza();
            plaza.MdiParent = this;
            plaza.WindowState = FormWindowState.Maximized;
            plaza.Show();
        }
    }
}