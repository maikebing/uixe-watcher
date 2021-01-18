using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher
{
    public partial class frmLogin : XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private bool _isMouseDown;
        private Point _formLocation; //form的location
        private Point _mouseOffset;

        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = true;
                _formLocation = Location;
                _mouseOffset = MousePosition;
            }
        }

        private void frmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
        }

        private void frmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            //鼠标的按下位置
            if (_isMouseDown)
            {
                Point pt = MousePosition;
                int x = _mouseOffset.X - pt.X;
                int y = _mouseOffset.Y - pt.Y;

                Location = new Point(_formLocation.X - x, _formLocation.Y - y);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            RuntimeSetting.NowCollect = new Dtos.User();
            ReloadTollInfo();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RuntimeSetting.NowCollect = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.LOGO;
            this.lblInfo.Text = "";
            ReloadTollInfo();
            this.txtPassword.Focus();
            RuntimeSetting.NowCollect = null;
        }

        private void ReloadTollInfo()
        {
            if (!string.IsNullOrEmpty(txtPlazaId.Text))
            {
                Task.Run(() =>
                {
                    try
                    {
                        var plazainfo = TollInfo.GetTollInfo(txtPlazaId.Text,true);
                        this.Invoke((MethodInvoker)delegate
                        {
                            Properties.Settings.Default.Save();
                            lblPlaza.Text =$"{plazainfo.station_name}车道监控";
                            lblInfo.Text = $"服务器IP:{plazainfo.ip } 站代码:{plazainfo.station_id}";
                        });
                    }
                    catch (Exception ex)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            lblInfo.Text = "未能获取站信息" + ex.Message;
                        });

                    }
                });

            }
            else
            {
                lblInfo.Text = "信息为空";
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin_Click(null, null);
        }
 
    }
}