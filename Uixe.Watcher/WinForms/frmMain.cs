using DevExpress.XtraBars;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher
{

    [ServiceLifetime(ServiceLifetime.Singleton)]
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly IServiceScopeFactory scopeFactor;
        private readonly AppSettings _setting;
        private readonly IServiceScope _scope;
        private readonly IMemoryCache _cache;
        private readonly frmLogin _login;
        private readonly ILogger logger;

        public frmMain(IServiceScopeFactory scopeFactor, IOptions<AppSettings> option, frmLogin login, ILogger<frmMain>  logger, IMemoryCache cache )
        {
            InitializeComponent();
            this.scopeFactor = scopeFactor;
            _setting = option.Value;
            _login = login;
            this.logger = logger;
            _scope = scopeFactor.CreateScope();
            _cache = cache;

        }

        private void btnOpenPlaza_ItemClick(object sender, ItemClickEventArgs e)
        { 
           
            _setting.PlaceType = PlaceType.Plaza;
           var _plaza= _scope.ServiceProvider.GetRequiredService<frmPlaza>();
            _plaza.MdiParent = this;
            _plaza.WindowState = FormWindowState.Maximized;
            _plaza.Show();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
            Application.ExitThread();
            Application.Exit();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private async void frmMain_Load(object sender, EventArgs e)
        {

            var who = await TollInfo.Guesswhoiam();
            this.Text = who?.name;
            if (who?.plazas.Count == 1)
            {
                logger.LogInformation($"单站运行{_setting.PlaceId}");
                _login.PlazaId = who?.plazas.FirstOrDefault()?.id;
                this.Visible = false;
                Login(_login.PlazaId);
            }
            else
            {

            }


        }


        public void Login(string plazaId)
        {
            //List<Dtos.Plaza> plazas = _setting.Plazas;
            if (_login.ShowDialog() == DialogResult.OK)
            {
                switch (_setting.PlaceType)
                {
                    case PlaceType.Plaza:
                        string name = $"{nameof(frmPlaza)}_{plazaId}";
                       var frm=  _cache.GetOrCreate(name, f=>
                        {
                            var frm =  new frmPlaza() { Name= name};
                            frm._runtimeSetting = _cache.Get<RuntimeSetting>(plazaId);
                            frm.FormClosed += Frm_FormClosed;
                            return frm;
                        });
                        frm.Show();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Application.Exit();
               
            }
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void Logout()
        {
          
        }
    }
}