using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Uixe.Watcher.WinForms
{
    public partial class frmSplashScreen : SplashScreen
    {
        public frmSplashScreen()
        {
            InitializeComponent();
            this.labelCopyright.Text = "Copyright © 2019-" + DateTime.Now.Year.ToString();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }
        public void SetDescription(string text)
        {
            labelStatus.Text = text;
        }

        #endregion

        public enum SplashScreenCommand
        {
        }
    }
}