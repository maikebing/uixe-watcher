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
using Uixe.Watcher.Extensions;
using Uixe.Watcher.Uitls;
using Uixe.Watcher.WinForms;

namespace Uixe.Watcher
{

    [ServiceLifetime(ServiceLifetime.Singleton)]
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly IServiceScopeFactory scopeFactor;
        private readonly AppSettings _setting;
        private readonly IServiceScope _scope;
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;

        public frmMain(IServiceScopeFactory scopeFactor, IOptions<AppSettings> option,  ILogger<frmMain>  logger, IMemoryCache cache )
        {
            InitializeComponent();
            this.scopeFactor = scopeFactor;
            _setting = option.Value;
             _logger = logger;
            _scope = scopeFactor.CreateScope();
            _cache = cache;

        }


        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _logger.LogInformation($"窗口已关闭， 开始关闭经常， 关闭原因为{e.CloseReason}");
            Application.ExitThread();
            Application.Exit();
        }
 

        private   async void frmMain_Load(object sender, EventArgs e)
        {
            frmSplashScreen wait =null;
             Invoke(() => {
                 wait = new frmSplashScreen();
                 wait.Show(this);
                 wait.SetDescription("正在加载初始化信息.....");
                 Application.DoEvents();
             });

            _logger.LogInformation("开始加载窗体....");
            await TollInfo.Guesswhoiam().ContinueWith(t =>
            {
                var who = _setting.whoiam;
                if (!t.IsFaulted && !t.IsCanceled)
                {
                    who = t.Result;
                    _setting.whoiam = who;
                    _setting.SaveUserAppSetting();
                    _logger.LogInformation("信息已保存。");
                }
                else
                {
                    _logger.LogWarning($"远程加载参数失败 IsFaulted:{t.IsFaulted} IsCanceled:{t.IsCanceled} {t.Exception?.Message  } -- {t.Exception?.InnerException?.Message}");
                    Invoke(() => wait.SetDescription("远程加载失败!"));
                    Application.DoEvents();
                }
               
                this.Invoke(() =>
                {
                    this.Text = $"{who.name}云坐席";
                    who?.plazas?.ForEach(p =>
                {
                    wait.SetDescription($"正在加载{p?.station_name}!");
                    LoadPlazaAsync(p);
                    Application.DoEvents();

                });
                });
                Invoke(() => wait.SetDescription("加载完成!"));
            }).ContinueWith(t =>
            {
                Invoke(() => wait?.Close());
            });
        }


        public    void  LoadPlazaAsync(Plaza plaza)
        {
            string name = $"{nameof(frmPlaza)}_{plaza.id}";
            var _runtimeSetting = _cache.GetOrCreate(plaza.id, c => new RuntimeSetting());
            _runtimeSetting.Plaza = plaza;
            var p = _runtimeSetting.Plaza;
            if (p != null && !string.IsNullOrEmpty(p.ip))
            {
                PlazaApi api = new(_runtimeSetting.Plaza.ip);
               
                var frm = _cache.GetOrCreate(name, f =>
                     {
                         var frm = new frmPlaza() { Name = name };
                         frm._runtimeSetting = _cache.Get<RuntimeSetting>(plaza.id);
                         frm.FormClosed += Frm_FormClosed;
                         return frm;
                      });
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmAbout f = new frmAbout();
            f.Show(this);
            f.Dispose();
        }

        private void btnLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.whoiam = _setting.whoiam;
            frmLogin._cache = _cache;
            frmLogin.Show(this);
        }
    }
}