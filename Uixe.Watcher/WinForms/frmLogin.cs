using DevExpress.XtraEditors;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher
{
    [ServiceLifetime(ServiceLifetime.Singleton)]
    public partial class frmLogin : XtraForm
    {
        private readonly IMemoryCache _cache;
        private readonly RuntimeSetting _runtimeSetting;

        public frmLogin( IMemoryCache cache, RuntimeSetting runtimeSetting)
        {
            _cache = cache;
            _runtimeSetting= runtimeSetting;
            InitializeComponent();
        }
        private bool _isMouseDown;
        private Point _formLocation; //form的location
        private Point _mouseOffset;

        public string PlazaId
        {
            get { return txtPlazaId.Text; }
            set { txtPlazaId.Text = value; }
        }

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
    
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            _ = ReloadTollInfoAsync();
            var p = _runtimeSetting.Plaza;
            if (p != null && !string.IsNullOrEmpty(p.ip))
            {
                PlazaApi api = new PlazaApi(_runtimeSetting.Plaza.ip);
                _runtimeSetting.Token = await api.SysLogin(txtUser.Text, txtPassword.Text, p.station_id, p.id);
                var result =await api.getRoleByUser(txtUser.Text, _runtimeSetting.Token?.token);
                if (!string.IsNullOrEmpty(_runtimeSetting.Token?.token))
                {
                    if (result.code == 0 && result.data != null && result.data.Any(f => f.roleId == 18))
                    {
                        _runtimeSetting.NowCollect = new Dtos.User() { UserId = txtUser.Text };
                        _runtimeSetting.UserRole = result.data;
                        _runtimeSetting.Token.LoginDateTime = DateTime.Now;
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        _runtimeSetting.Token.LoginDateTime = DateTime.MinValue;
                        lblInfo.Text = $"没有找到TCO角色(18)";
                    }
                }
                else
                {
                    _runtimeSetting.Token.LoginDateTime = DateTime.MinValue;
                    lblInfo.Text = $"{_runtimeSetting.Token?.code} {_runtimeSetting.Token?.msg}";
                }
            }
            else
            {
                lblInfo.Text = "站信息错误";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _runtimeSetting.NowCollect = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void frmLogin_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.LOGO;
            this.lblInfo.Text = "";

            await ReloadTollInfoAsync();
            txtPlazaId.Text = _cache.Get<string>("plazaid" );
            this.txtPassword.Focus();
        }

        private async  Task ReloadTollInfoAsync()
        {
            if (!string.IsNullOrEmpty(txtPlazaId.Text))
            {
                await Task.Run(async () =>
                 {
                     try
                     {
                         var plazainfo =await TollInfo.GetTollInfo(txtPlazaId.Text, true);
                         _runtimeSetting.Plaza = plazainfo;
                         this.Invoke((MethodInvoker)delegate
                         {
                             Properties.Settings.Default.Save();
                             lblPlaza.Text = $"{plazainfo.station_name}车道监控";
                             lblserver.Text = $"服务器IP:{plazainfo.ip } 站代码:{plazainfo.station_id}";
                         });
                         _cache.Set("plazaid", txtPlazaId.Text, TimeSpan.FromDays(30));
                     }
                     catch (Exception ex)
                     {
                         this.Invoke((MethodInvoker)delegate
                         {
                             lblserver.Text = "未能获取站信息" + ex.Message;
                         });
                     }
                 });
            }
            else
            {
                lblserver.Text = "信息为空";
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