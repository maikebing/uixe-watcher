using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Controls;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Ring;
using Uixe.Watcher.TCO;
using Uixe.Watcher.Uitls;
namespace Uixe.Watcher
{
    public partial class frmPlaza : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public int cbSize;
            public int dwTime;
        }

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        public double GetIdleTime()
        {
            LASTINPUTINFO lii = new LASTINPUTINFO();
            lii.cbSize = Marshal.SizeOf(lii.GetType());
            GetLastInputInfo(ref lii);
            return (Environment.TickCount - lii.dwTime) / 1000.0;
        }

        #region 加载

        public frmPlaza()
        {
            InitializeComponent();
        }

     

        public frmShowTCOCall _tcocall;
        public frmWeightTCOCall WeightTCOCall;

        public Plaza Plaza => _runtimeSetting?.Plaza;
        public RuntimeSetting _runtimeSetting { get; set; }
        private void frmMain_Load(object sender, EventArgs e)
        {
            btnUpgrade.Visibility = BarItemVisibility.Never;
         
     
            this.Icon = Properties.Resources.LOGO;
            if (System.IO.Directory.Exists("Ring"))
            {
                foreach (var item in System.IO.Directory.GetFiles(AppContext.BaseDirectory + "\\Ring", "*.wav"))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(item);
                    BarButtonItem bt = new BarButtonItem() { Caption = fi.Name.Replace(fi.Extension, "") };
                    bt.ItemClick += (object sender1, ItemClickEventArgs e1) =>
                    {
                        BarButtonItem btx = (BarButtonItem)e1.Item;
                        if (btx != null)
                        {
                            btnRing.Caption = "铃声:" + btx.Caption;
                            _runtimeSetting.Ring = btx.Caption;
                            Task.Run(() =>
                            {
                                PlayUitls.SetMp3File(_runtimeSetting.Ring);
                                PlayUitls.PlayRing();
                            });
                        }
                    };
                    btnRing.AddItem(bt);
                }
                btnRing.Caption = "铃声:" + _runtimeSetting.Ring;
                PlayUitls.SetMp3File(_runtimeSetting.Ring);
                btnRing.Enabled = true;
            }
            else
            {
                btnRing.Enabled = false;
            }

            ShowStatusInfo("就绪");
            try
            {
                SpeechUtils.Speecher.GetInstalledVoices().ToList().ForEach(iv =>
                {
                    var barItem = new BarCheckItem(menuVoiceList.Manager) { Caption = iv.VoiceInfo.Name, Enabled = iv.Enabled, Description = iv.VoiceInfo.Description };

                    barItem.ItemClick += delegate (object sender1, ItemClickEventArgs e1)
                    {
                        SpeechUtils.Speecher.SelectVoice(e1.Item.Caption);
                        ResetSpeachMenu();
                    };
                    menuVoiceList.AddItem(barItem);
                });
                ResetSpeachMenu();
                SpeechUtils.Speecher.SpeakCompleted += Speech_SpeakCompleted;
            }
            catch (Exception)
            {
                menuVoiceList.Enabled = false;
                btnTest.Enabled = false;
            }
            LoadLaneInfo();
        }

        public void LoadLaneInfo(bool reset = false)
        {
            this.SuspendLayout();
            LoadLaneView(Plaza);
            UserAccessControl();
            this.ResumeLayout();
            this.Activate();

        }
         

        private void ResetSpeachMenu()
        {
            menuVoiceList.ItemLinks.ToList().ForEach(baritem =>
            {
                if (baritem != null && baritem.Item is BarCheckItem barchk)
                {
                    barchk.Checked = SpeechUtils.Speecher.Voice.Name == barchk.Caption;
                    if (barchk.Checked)
                    {
                        menuVoiceList.Caption = "播音员：" + barchk.Caption;
                    }
                }
            });
        }

        /// <summary>
        /// 加载车道
        /// </summary>
        private void LoadLaneView(Plaza plaza)
        {
            ShowStatusInfo("正在加载车道列表");
            lanView.SuspendLayout();
            messageView.SuspendLayout();
            lanView.InitLaneInfo(plaza,_logger,_runtimeSetting);
            messageView.initMessageView(plaza.id, 100);
            messageView.ResumeLayout(false);
            lanView.ResumeLayout(false);
            Application.DoEvents();
            ShowStatusInfo("就绪");
        }
        /// <summary>
        /// 用户权限控制
        /// </summary>
        public void UserAccessControl()
        {
            var p = Plaza;
            this.Text = string.Format($"{p.road_name}-{p.station_name}({p.id}) {_runtimeSetting.NowCollect?.UserId} ");
            this.Ribbon.ApplicationCaption = this.Text;

        }

        #endregion 加载

        #region 卸载


        public void Login()
        {


            UserAccessControl();
            this.btnRBLogin.Enabled = false;

            this.btnRBLogout.Enabled = true;

        }

        public void Logout()
        {
            UserAccessControl();
            btnRBLogin.Enabled = true;
            btnRBLogout.Enabled = false;
        }

        #endregion 卸载

        /// <summary>
        /// 显示状态信息
        /// </summary>
        /// <param name="text">要显示的内容</param>
        public void ShowStatusInfo(string text)
        {
            _logger.LogInformation(text);
            this.Invoke((MethodInvoker)delegate
            {
                if (!this.IsDisposed && this.IsHandleCreated)
                {
                    this.txtStatus.Caption = text;
                }
                Application.DoEvents();
            });
        }




        private void frmMain_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
        }


        private void btnSyncTime_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void btnAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        public void Alert(string caption, string text)
        {
            _logger.LogWarning($"Alert {caption} {text}");
            this.Invoke((MethodInvoker)delegate
            {
                acMsg.Show(this, caption, text);
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="plaza">6500226</param>
        /// <param name="laneno">6500225X01</param>
        /// <param name="revdata">json</param>
        public void ShowLaneInfor(string plaza, string laneno, LaneStatus revdata)
        {
            lock (lanView)
            {
             lanView .ShowLaneInfor($"{plaza}{laneno}", revdata);
               
            }
        }

        public void ShowLaneLost(string plaza, string laneno)
        {
            lock (lanView)
            {

                lanView.ShowLaneLost(laneno);
             
            }
        }

        public void ShowMessageView(MsgInfo mi)
        {
            messageView.ShowMessageView(mi);
        }
 

        private void skinRibbonGalleryBarItem2_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            _runtimeSetting.SkinStyle = e.Item.Tag.ToString();

        }

        private DateTime lastsend = DateTime.MinValue;
        internal ILogger _logger;
        internal ILoggerFactory _loggerFactory;
        internal IMemoryCache  _cache;
        private void btnSend_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DateTime.Now.Subtract(lastsend).TotalSeconds > 2)
            {
                string msg = txtMsg.EditValue as string;
                if (!string.IsNullOrEmpty(msg))
                {

                    //BLLWatcher.AllLaneSend(msg);
                }
                else
                {
                    XtraMessageBox.Show("不能发送空内容");
                }
            }
            else
            {
                XtraMessageBox.Show("每条消息之间必须间隔两秒，避免影响收费");
            }
        }

        private void btnUpgrade_ItemClick(object sender, ItemClickEventArgs e)
        {
           
           
        }


        [DebuggerStepThrough]
        private void tmNetworkTest_TickAsync(object sender, EventArgs e)
        {
            try
            {

                lanView.SendHeartBeat();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "心跳无法发送");
            }
        }

        private void btnTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            SpeechUtils.Speecher.SpeakAsync("E01出现黑名单车辆");//语音阅读方法
        }

        private void Speech_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            btnTest.Enabled = true;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 软件更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnUpgrade.PerformClick();
        }
    }//frmMain
}