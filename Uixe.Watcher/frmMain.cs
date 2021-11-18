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
        private readonly frmPlaza _plaza;

        public frmMain(frmPlaza plaza)
        {

            _plaza = plaza;
            InitializeComponent();
        }

        private void btnOpenPlaza_ItemClick(object sender, ItemClickEventArgs e)
        {
            _plaza.MdiParent = this;
            _plaza.WindowState = FormWindowState.Maximized;
            _plaza.Show();
        }
    }
}