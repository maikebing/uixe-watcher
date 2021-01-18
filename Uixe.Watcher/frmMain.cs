using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using Uixe.Watcher.Ring;
using Uixe.Watcher.V1;
using System.Linq;
using Uixe.Watcher.Tools;
using System.Diagnostics;
using System.Reflection;
using Uixe.Watcher.Controls;
using WindowsFirewallHelper;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.Deployment.Application;
using System.Speech.Synthesis;
using System.Drawing;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Uitls;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace Uixe.Watcher
{
    //[System.Diagnostics.DebuggerStepThrough]
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public int cbSize;
            public int dwTime;
        }

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        public static double GetIdleTime()
        {
            LASTINPUTINFO lii = new LASTINPUTINFO();
            lii.cbSize = Marshal.SizeOf(lii.GetType());
            GetLastInputInfo(ref lii);
            return (Environment.TickCount - lii.dwTime) / 1000.0;
        }

        public static frmTimeSync TimeSync;


        #region 加载

        public frmMain()
        {
            InitializeComponent();
        }

        private CougarClockRepositoryItem repositoryItem = new CougarClockRepositoryItem();
        private CougarClockContainer control = new CougarClockContainer();
        private BarEditItem barEditItem = new BarEditItem();
        private Plaza Plaza { get; set; }
        IMqttClient client;
        private void frmMain_Load(object sender, EventArgs e)
        {
            StarupMqttClient();
            btnUpgrade.Visibility = ApplicationDeployment.IsNetworkDeployed ? BarItemVisibility.Always : BarItemVisibility.Never;
            repositoryItem.ControlType = control.GetType();
            barEditItem.Edit = repositoryItem;
            barEditItem.EditHeight = control.Height;
            barEditItem.Width = control.Width;
            rpgTime.ItemLinks.Add(barEditItem);

            this.Icon = Properties.Resources.LOGO;
            this.SuspendLayout();
            this.btnLogout.Enabled = false;

            Plaza = TollInfo.GetTollInfo();
            LoadLaneView(Plaza);
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
                            Properties.Settings.Default.Ring = btx.Caption;
                            PlayUitls.SetMp3File(Properties.Settings.Default.Ring);
                            PlayUitls.PlayRing(null);
                        }
                    };
                    btnRing.AddItem(bt);
                }
                btnRing.Caption = "铃声:" + Properties.Settings.Default.Ring;
                PlayUitls.SetMp3File(Properties.Settings.Default.Ring);
                btnRing.Enabled = true;
            }
            else
            {
                btnRing.Enabled = false;
            }
            UserAccessControl();
            this.ResumeLayout();
            this.Activate();
            ShowStatusInfo("就绪");
            freshAgriProductsBindingSource.DataSource = FAP.GetFreshAgriProducts.FreshAgriProducts.ToArray();
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

        }

        private void StarupMqttClient()
        {
            Task.Run(async () =>
            {
                var p = Uitls.TollInfo.GetTollInfo();
                var factory = new MqttFactory();
                client = factory.CreateMqttClient();
                var options = new MqttClientOptionsBuilder()
        .WithTcpServer(p.ip, 1883)
        .WithCredentials($"tco_{p.id}", "")
        .WithClientId(p.id)
        .Build();
                client.UseDisconnectedHandler(async xe =>
                {
                    Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                    await Task.Delay(TimeSpan.FromSeconds(5));

                    try
                    {
                        await client.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken
                    }
                    catch
                    {
                        Console.WriteLine("### RECONNECTING FAILED ###");
                    }
                });
                client.UseApplicationMessageReceivedHandler(h =>
                {
                    if (h.ApplicationMessage.Topic.StartsWith("/lane/emrc_main/status/"))
                    {
                        string t = h.ApplicationMessage.Topic.Split('/').Last();
                        var plaza = t.Substring(0, 7);
                        var laneno = t.Substring(7, 3);
                        ShowLaneInfor(plaza, t, System.Text.Encoding.GetEncoding(936).GetString(h.ApplicationMessage.Payload));
                    }
                });
                client.UseConnectedHandler(async h =>
                {
                    await client.SubscribeAsync("/lane/emrc_main/status/+");
                });
                await client.ConnectAsync(options, CancellationToken.None);
            });
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
            lanView.TabPages.Clear();
            lanView.SuspendLayout();
            messageView.SuspendLayout();
            List<Plaza> plasas = new List<Plaza>();
            int maxlanecount = 0;
            plasas.Add(plaza);
            lanView.SuspendLayout();
            //如果本站不在配置列表中， 则临时加一个。
            if (plasas.Count == 0)
            {
                plasas.Add(new Plaza()
                {
                    road_name = "错误",
                    id = "6509999"
                });
            }
            foreach (var item in plasas)
            {
                XtraTabPage xtp = lanView.TabPages.Add();
                xtp.Name = item.id;
                xtp.Text = string.Format("{0}-{1}", item.road_name, item.station_name);
                Uixe.Watcher.Controls.LaneView lv = new Uixe.Watcher.Controls.LaneView();
                xtp.Controls.Add(lv);
                lv.Name = item.id;
                lv.InitLaneInfo(item);

                if (lv.LaneCount > maxlanecount)
                {
                    maxlanecount = lv.LaneCount;
                }
                lv.Dock = DockStyle.Top;
                messageView.initMessageView(item.id, 100);
            }

            if (lanView.TabPages.Count > 1)
            {
                lanView.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            }
            int el = 0;
            Control[] cont = lanView.SelectedTabPage.Controls.Find(plasas[0].id, true);
            if (cont.Length > 0)
            {
                var lv = ((Uixe.Watcher.Controls.LaneView)cont[0]);
                el = lv.LaneCount;
                if (el > 0)
                {
                    lanView.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                }
                else
                {
                    sccMain.SplitterPosition = lanView.Height;
                }

                sccMain.SplitterPosition = lv.Height;
            }

            lanView.SelectedTabPage = null;
            if (lanView.TabPages.Count > 0) lanView.SelectedTabPage = lanView.TabPages[0];
            if (lanView.SelectedTabPage != null) messageView.SetPlaza(lanView.SelectedTabPage.Name);

            messageView.ResumeLayout(false);
            lanView.ResumeLayout(false);

            Application.DoEvents();
            ShowStatusInfo("就绪");
        }


        /// <summary>
        /// 用户权限控制
        /// </summary>
        private void UserAccessControl()
        {
            var p = Uitls.TollInfo.GetTollInfo();
            this.Text = string.Format($"{p.road_name}-{p.station_name}({p.id}) v{ Assembly.GetExecutingAssembly().GetName().Version}");
            this.Ribbon.ApplicationCaption = this.Text;

        }

        #endregion 加载

        #region 卸载

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.SkinStyle = UserLookAndFeel.Default.ActiveSkinName;
            Properties.Settings.Default.Save();
            Application.ExitThread();
            Application.Exit();
        }

        public void Login()
        {
            frmLogin lg = new frmLogin();
            lg.ShowDialog();
            UserAccessControl();
            if (RuntimeSetting.NowCollect != null && RuntimeSetting.NowCollect.UserId.Trim().Length > 0)
            {
                this.btnRBLogin.Enabled = false;
                this.btnLogin.Enabled = false;

                this.btnLogout.Enabled = true;
                this.btnRBLogout.Enabled = true;
            }
        }

        public void Logout()
        {
            RuntimeSetting.NowCollect = new User();
            UserAccessControl();
            this.btnLogout.Enabled = false;
            btnRBLogin.Enabled = true;
            btnRBLogout.Enabled = false;
            this.btnLogin.Enabled = true;
            if (TimeSync != null && TimeSync.IsDisposed == false) TimeSync.Close();
            Properties.Settings.Default.SkinStyle = UserLookAndFeel.Default.ActiveSkinName;
        }

        #endregion 卸载

        /// <summary>
        /// 显示状态信息
        /// </summary>
        /// <param name="text">要显示的内容</param>
        public void ShowStatusInfo(string text)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.txtStatus.Caption = text;
                Application.DoEvents();
            });
        }

        private int autosynctime = -1;

        private void time_Tick(object sender, EventArgs e)
        {
            if (autosynctime != System.DateTime.Now.Hour)
            {
                autosynctime = System.DateTime.Now.Hour;
            }

            if (GetIdleTime() >= TimeSpan.FromMinutes(30).TotalSeconds)
            {
                Logout();
            }
        }

        private void frmMain_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Help.ShowHelp(this, "TCOHelp.chm");
        }



        private void btnLogin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Login();
        }

        private void btnLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Logout();
        }


        private void btnSyncTime_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TimeSync = new frmTimeSync();
            TimeSync.ShowDialog(this);
        }



        private void btnAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
            f.Dispose();
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }


        public void Alert(string caption, string text)
        {
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
        public void ShowLaneInfor(string plaza, string laneno, string revdata)
        {
            lock (lanView)
            {
                var tb = from p in lanView.TabPages.ToArray() where p.Name == plaza select p;
                if (tb.Any())
                {
                    XtraTabPage xtp = tb.First();
                    Control[] cont = xtp.Controls.Find(plaza, true);
                    if (cont.Length > 0)
                    {
                        ((Uixe.Watcher.Controls.LaneView)cont[0]).ShowLaneInfor(laneno, revdata);
                    }
                }
            }
        }

        public void ShowMessageView(MsgInfo mi)
        {
            messageView.ShowMessageView(mi);
        }

        private void lanView_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.Page != null && !string.IsNullOrEmpty(e.Page.Name) && !string.IsNullOrEmpty(e.Page.Text))
            {
                messageView.SetPlaza(lanView.SelectedTabPage.Name);
                Control[] cont = lanView.SelectedTabPage.Controls.Find(e.Page.Name, true);
                if (cont.Length > 0)
                {
                    var lv = ((Uixe.Watcher.Controls.LaneView)cont[0]);
                    int maxlanecount = lv.LaneCount;
                    int maxe = lv.LaneCount;
                    if (lanView.TabPages.Count > 1)
                    {
                        lanView.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
                        sccMain.SplitterPosition = lv.Height;
                    }
                    //else if (AppConfig.RunTime.BreakShow && maxe > 0)
                    //{
                    //    lanView.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                    //    sccMain.SplitterPosition = lv.Height;
                    //}
                    else
                    {
                        lanView.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                        sccMain.SplitterPosition = lanView.Height;
                    }
                }
            }
        }




        private void skinRibbonGalleryBarItem2_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            Properties.Settings.Default.SkinStyle = e.Item.Tag.ToString();
            Properties.Settings.Default.Save();
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



        private DateTime lastsend = DateTime.MinValue;

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
            AppUtils.InstallUpdateSyncWithInfo();
        }

        private int min = -1;
        private int hou = -1;



        private async  void tmNetworkTest_TickAsync(object sender, EventArgs e)
        {
            if (min != DateTime.Now.Minute)
            {
                try
                {
                    min = DateTime.Now.Minute;
                    var p = new Ping();
                    string ipaddress = Plaza.ip;
                    var pr = await p.SendPingAsync(ipaddress);
                    try
                    {
                        if (pr.Status != IPStatus.Success)
                        {
                            chkServerStatus.EditValue = false;
                            chkServerStatus.Caption = $"服务器{ipaddress}网络故障,{pr.Status}";
                            Alert("网络故障", chkServerStatus.Caption);
                        }
                        else
                        {
                            chkServerStatus.EditValue = true;
                            chkServerStatus.Caption = "服务器网络正常";
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                catch (Exception)
                {
                }
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