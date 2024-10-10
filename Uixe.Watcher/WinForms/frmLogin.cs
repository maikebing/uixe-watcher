using DevExpress.XtraEditors;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher
{

    public partial class frmLogin : XtraForm
    {


        public frmLogin( )
        {
            InitializeComponent();
        }
   
        private bool _isMouseDown;
        private Point _formLocation; //form的location
        private Point _mouseOffset;
        internal T_Boss whoiam;
        internal IMemoryCache _cache;

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
    
        private  void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            string password = txtPassword.Text;
            btnLogin.Enabled = false;
            try
            {
                whoiam.Plazas?.ForEach(async plaza =>
                {
                    var _runtimeSetting = _cache.Get<RuntimeSetting>(plaza.Id);
                    string name = $"{nameof(frmPlaza)}_{plaza.Id}";
                    var winplaza = _cache.Get<frmPlaza>(name);
                    if (plaza != null && !string.IsNullOrEmpty(plaza.Ip))
                    {
                        PlazaApi api = new(plaza.Ip);
                        _runtimeSetting.Token = await api.SysLogin(user, password, plaza.StationId, plaza.Id);
                        if (!string.IsNullOrEmpty(_runtimeSetting.Token?.token))
                        {
                            var result = await api.getRoleByUser(user, _runtimeSetting.Token?.token);
                            if (result.code == 0 && result.data != null && result.data.Any(f => f.roleId == 18))
                            {
                                _runtimeSetting.NowCollect = new Dtos.User() { UserId = user };
                                _runtimeSetting.UserRole = result.data;
                                _runtimeSetting.Token.LoginDateTime = DateTime.Now;
                                _cache.Set(plaza.Id, _runtimeSetting);
                                winplaza.Invoke(() => winplaza.UserAccessControl());
                                winplaza.Invoke(() => winplaza.Alert($"登录{plaza.StationName}成功", $"{_runtimeSetting.Token?.code} {_runtimeSetting.Token?.msg}"));
                            }
                            else
                            {

                                winplaza.Invoke(() => winplaza.Alert($"登录{plaza.StationName}失败", $"{_runtimeSetting.Token?.code} {_runtimeSetting.Token?.msg}"));
                            }
                        }
                        else
                        {
                            winplaza.Invoke(() => winplaza.Alert($"登录{plaza.StationName}错误", $"{_runtimeSetting.Token?.code} {_runtimeSetting.Token?.msg}"));
                        }
                    }
                    else
                    {
                        winplaza.Invoke(() => winplaza.Alert($"{plaza.StationName}配置错误", $"{_runtimeSetting.Token?.code} {_runtimeSetting.Token?.msg}"));
                    }
                });
                this.Close();
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private   void frmLogin_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.LOGO;
            this.lblInfo.Text = "";
            this.txtPassword.Focus();
        }
 

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin_Click(null, null);
        }
         
    }
}