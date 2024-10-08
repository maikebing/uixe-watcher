using DevExpress.XtraBars;
using LibVLCSharp.Shared;
using LiteDB;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Controls;
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
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private readonly ConnectionString _connection;
        private CougarClockRepositoryItem repositoryItem = new CougarClockRepositoryItem();
        private CougarClockContainer control = new CougarClockContainer();
        private BarEditItem barEditItem = new BarEditItem();
        public frmMain(IServiceScopeFactory scopeFactor, IOptions<AppSettings> option,  ILogger<frmMain>  logger, IMemoryCache cache , ILoggerFactory loggerFactory, LiteDB.ConnectionString connection)
        {
            InitializeComponent();
            this.scopeFactor = scopeFactor;
            _setting = option.Value;
             _logger = logger;
            _scope = scopeFactor.CreateScope();
            _cache = cache;
            _loggerFactory = loggerFactory;
            repositoryItem.ControlType = control.GetType();
            barEditItem.Edit = repositoryItem;
            barEditItem.EditHeight = control.Height;
            barEditItem.Width = control.Width;
            rpgTime.ItemLinks.Add(barEditItem);
            _connection = connection;
            var libvlcpath =new DirectoryInfo( Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "libvlc"));
            if (!libvlcpath.Exists )
            {
                try
                {
                    _logger.LogInformation($"VLC不存在， 正在解压libvlc至{libvlcpath.FullName}");
                    using var stem = new MemoryStream(libvlc_zip.Properties.Resources.libvlc);
                    using ZipArchive zip = new ZipArchive(stem);
                    zip.ExtractToDirectory(libvlcpath.FullName,true);
                    _logger.LogInformation($"VLC已经解压libvlc至{libvlcpath.FullName}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"VLC解压libvlc至{libvlcpath.FullName}时遇到错误{ex.Message}");
                }
            }
            libvlcpath.Refresh();
            if (libvlcpath.Exists)
            {
                Core.Initialize(libvlcpath.FullName);
            }
            else
            {
                _logger.LogInformation($"VLC已经不存在");
            }
        }
        private string temptime = null;
        private object timeobjlock = new object();
        [DebuggerStepThrough]
        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (timeobjlock)
            {
                string timetemp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (timetemp != temptime)
                {
                    temptime = timetemp;
                    barEditItem.EditValue = temptime;
                }
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _logger.LogInformation($"窗口已关闭， 开始关闭经常， 关闭原因为{e.CloseReason}");
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
            await TollInfo.GuessMyInfo().ContinueWith(t =>
            {
                var who = _setting.whoiam;
                if (!t.IsFaulted && !t.IsCanceled)
                {
                    if (t.Result!=null && t.Result.code==200)
                    {
                        who = t.Result.data;
                        _setting.whoiam = who;
                        _setting.SaveUserAppSetting();
                        _logger.LogInformation("信息已保存。");
                    }
                    else
                    {
                        _logger.LogWarning($"远程返回错误{t.Result.code}-{t.Result.msg}");
                        Invoke(() => wait.SetDescription($"远程返回错误{t.Result.code}-{t.Result.msg}"));
                        Application.DoEvents();
                    }
                    
                }
                else
                {
                    _logger.LogWarning($"远程加载参数失败 IsFaulted:{t.IsFaulted} IsCanceled:{t.IsCanceled} {t.Exception?.Message  } -- {t.Exception?.InnerException?.Message}");
                    Invoke(() => wait.SetDescription("远程加载失败!"));
                    Application.DoEvents();
                }
               
                this.Invoke(() =>
                {
                    this.Text = $"{(who?.Name??"(none)")}远程值守";
                
                        wait.SetDescription($"正在加载{(who?.Name ?? "(none)")}!");
                        LoadPlaza(who);
                        Application.DoEvents();
                });
                Invoke(() => wait.SetDescription("加载完成!"));
            }).ContinueWith(t =>
            {
                Invoke(() => wait?.Close());
            });
        }

        public void LoadPlaza(T_Boss tb)
        {
            string name = $"{nameof(frmPlaza)}_{tb.Id}";
            var frm = _cache.GetOrCreate(name, f =>
            {
                var _log = _loggerFactory.CreateLogger(name);
                var frm = new frmPlaza() { Name = name, _logger = _log, _loggerFactory = _loggerFactory, _cache = _cache, settings = _setting, _connection = _connection };
                frm.FormClosed += Frm_FormClosed;
                frm.Boss = tb;
                return frm;
            });
            tb.Plazas.ForEach(p =>
            {
                _cache.Set($"{nameof(frmPlaza)}_{p.Id}",frm);
            } );
            frm.MdiParent = this;
            frm.Show();
        }
        public    void  LoadPlaza(T_Plaza plaza)
        {
            string name = $"{nameof(frmPlaza)}_{plaza.Id}";
            var _runtimeSetting = _cache.GetOrCreate(plaza.Id, c => new RuntimeSetting());
            _runtimeSetting.Plaza = plaza;
            var p = _runtimeSetting.Plaza;
            if (p != null && !string.IsNullOrEmpty(p.Ip))
            {
                PlazaApi api = new(_runtimeSetting.Plaza.Ip);
                var frm = _cache.GetOrCreate(name, f =>
                     {
                         var _log= _loggerFactory.CreateLogger(name);
                          var frm = new frmPlaza() { Name = name ,_logger= _log, _loggerFactory= _loggerFactory, _cache = _cache ,settings= _setting ,  _connection = _connection };
                         frm._runtimeSetting = _cache.Get<RuntimeSetting>(plaza.Id);
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