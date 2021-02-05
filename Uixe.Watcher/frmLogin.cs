using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Linq;
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

            _ = ReloadTollInfoAsync();
            var p = RuntimeSetting.Plaza;
            if (p != null && !string.IsNullOrEmpty(p.ip))
            {
                PlazaApi api = new PlazaApi(RuntimeSetting.Plaza.ip);
                RuntimeSetting.Token = api.SysLogin(txtUser.Text, txtPassword.Text, p.station_id, p.id);
               var result=  api.getRoleByUser(txtUser.Text);
                if (!string.IsNullOrEmpty(RuntimeSetting.Token?.token))
                {
                    if (result.code==0  && result.data!=null && result.data.Any(f=>f.roleId==18))
                    {
                        RuntimeSetting.UserRole = result.data;
                        RuntimeSetting.Token.LoginDateTime = DateTime.Now;
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        RuntimeSetting.Token.LoginDateTime = DateTime.MinValue;
                        lblInfo.Text = $"没有找到TCO角色(18)";
                    }

                 
                }
                else
                {
                    RuntimeSetting.Token.LoginDateTime = DateTime.MinValue;
                    lblInfo.Text = $"{RuntimeSetting.Token?.code} {RuntimeSetting.Token?.msg}";
                }
            }
            else
            {
                lblInfo.Text = "站信息错误";
            }
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
            _ = ReloadTollInfoAsync();
            this.txtPassword.Focus();
        }

        private async Task ReloadTollInfoAsync()
        {
            if (!string.IsNullOrEmpty(txtPlazaId.Text))
            {
                await Task.Run(() =>
                 {
                     try
                     {
                         var plazainfo = TollInfo.GetTollInfo(txtPlazaId.Text, true);
                         RuntimeSetting.Plaza = plazainfo;
                         this.Invoke((MethodInvoker)delegate
                         {
                             Properties.Settings.Default.Save();
                             lblPlaza.Text = $"{plazainfo.station_name}车道监控";
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

        private void txtPlazaId_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}