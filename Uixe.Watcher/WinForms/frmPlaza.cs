using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Microsoft.Extensions.DependencyInjection;
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

        private CougarClockRepositoryItem repositoryItem = new CougarClockRepositoryItem();
        private CougarClockContainer control = new CougarClockContainer();
        private BarEditItem barEditItem = new BarEditItem();

        public frmShowTCOCall _tcocall;
        public frmWeightTCOCall WeightTCOCall;

        public Plaza Plaza => _runtimeSetting?.Plaza;
        public RuntimeSetting _runtimeSetting { get; set; }
        private void frmMain_Load(object sender, EventArgs e)
        {
            btnUpgrade.Visibility = BarItemVisibility.Never;
            repositoryItem.ControlType = control.GetType();
            barEditItem.Edit = repositoryItem;
            barEditItem.EditHeight = control.Height;
            barEditItem.Width = control.Width;
            rpgTime.ItemLinks.Add(barEditItem);

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
                            Properties.Settings.Default.Ring = btx.Caption;
                            Task.Run(() =>
                            {
                                PlayUitls.SetMp3File(Properties.Settings.Default.Ring);
                                PlayUitls.PlayRing();
                            });
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

        private void StarupMqttClient()
        {
            //Task.Run(async () =>
            //{
            //    var p = Uitls.TollInfo.GetTollInfo(_runtimeSetting.Plaza?.id);
            //    var factory = new MqttFactory();
            //    client = factory.CreateMqttClient();
            //    string ipaddress = Program.mqttserver ? "127.0.0.1" : p.ip;
            //    var options = new MqttClientOptionsBuilder()
            //        .WithCredentials($"tco_{p.id}", "")
            //        .WithTcpServer(ipaddress, 1883)
            //        .WithClientId(p.id)
            //        .WithWillMessage(new MqttApplicationMessage() { Topic = "/tco/willmessage", QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce, Retain = true })
            //        .Build();
            //    chkServerStatus.Caption = $"服务器:{p.ip}";

            //    client.UseDisconnectedHandler(async xe =>
            //    {
            //        Console.WriteLine("### DISCONNECTED FROM SERVER ###");
            //        await Task.Delay(TimeSpan.FromSeconds(1));

            //        this.Invoke((MethodInvoker)delegate
            //        {
            //            chkServerStatus.EditValue = false;

            //            chkServerStatus.Caption = $"服务器{ipaddress}网络故障,{xe.Reason}";
            //            Alert("网络故障", chkServerStatus.Caption);
            //        });
            //        if (!xe.ClientWasConnected)
            //        {
            //            try
            //            {
            //                await client.ReconnectAsync();
            //            }
            //            catch
            //            {
            //                Console.WriteLine("### RECONNECTING FAILED ###");
            //            }
            //        }
            //    });
            //    client.UseApplicationMessageReceivedHandler(h =>
            //    {
            //        var message = System.Text.Encoding.GetEncoding(936).GetString(h.ApplicationMessage.Payload);
            //        Console.WriteLine($"{h.ApplicationMessage.Topic}");
            //        if (h.ApplicationMessage.Topic == "/lane/emrc_main/willmessage" && message.Length >= 10)
            //        {
            //            try
            //            {
            //                var plaza = message.Substring(0, 7);
            //                var laneno = message.Substring(7, 3);
            //                ShowLaneLost(plaza, laneno);
            //            }
            //            catch (Exception ex)
            //            {

            //                Debug.WriteLine($"willmessage:{ex.Message}");
            //            }
            //        }
            //        else if (h.ApplicationMessage.Topic.StartsWith("/lane/emrc_main/status/"))
            //        {
            //            try
            //            {


            //            string t = h.ApplicationMessage.Topic.Split('/').Last();
            //            var plaza = t.Substring(0, 7);
            //            var laneno = t.Substring(7, 3);
            //            ShowLaneInfor(plaza, t, message);
            //            }
            //            catch (Exception ex)
            //            {

            //                Debug.WriteLine($"emrc_main_status:{ex.Message}");
            //            }
            //        }
            //        else if (h.ApplicationMessage.Topic.StartsWith("/lane/emrc_main/confirm/"))
            //        {
            //            Task.Run(() =>
            //            {
            //                try
            //                {
            //                    this.Invoke((MethodInvoker)delegate
            //                    {
            //                        this.ShowTCOInfo(h.ApplicationMessage.Topic, message, client);
            //                    });
            //                }
            //                catch (Exception ex)
            //                {
            //                    Debug.WriteLine($"confirm:{ex.Message}");
            //                }
            //            });
            //        }
            //        else if (h.ApplicationMessage.Topic.StartsWith("/lane/emrc_main/message/"))
            //        {
            //            try
            //            {
            //                ShowMessageView(JsonConvert.DeserializeObject<MsgInfo>(message, new JsonSerializerSettings() { DateTimeZoneHandling = DateTimeZoneHandling.Local }));
            //            }
            //            catch (Exception ex)
            //            {

            //                Debug.WriteLine($"emrc_main/message:{ex.Message}");
            //            }
            //        }
            //    });
            //    client.UseConnectedHandler(async h =>
            //    {
            //        try
            //        {


            //            await client.SubscribeAsync(
            //                new MqttTopicFilter() { Topic = "/lane/emrc_main/confirm/+", QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce },
            //                new MqttTopicFilter() { Topic = "/lane/emrc_main/status/+", QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce },
            //                new MqttTopicFilter() { Topic = "/lane/emrc_main/message/", QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce }
            //                );
            //            var pubresult = await client.PublishAsync("/tco/status/", new { message = "startup" });
            //            this.Invoke((MethodInvoker)delegate
            //            {
            //                chkServerStatus.EditValue = true;
            //                chkServerStatus.EditValue = true;
            //                chkServerStatus.Caption = $"服务器{ipaddress}网络恢复";
            //            });
            //        }
            //        catch (Exception ex)
            //        {
            //            Debug.WriteLine($"UseConnectedHandler:{ex.Message}");

            //        }
            //    });

            //    await client.ConnectAsync(options, CancellationToken.None);
            //    do
            //    {
            //        try
            //        {
            //            if (!client.IsConnected)
            //            {
            //                Debug.WriteLine($"ReconnectAsync");
            //                await client.ReconnectAsync();
            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            Debug.WriteLine($"ReconnectAsync:{ex.Message}");
            //        }
            //        Thread.Sleep(TimeSpan.FromSeconds(10));
            //    } while (true);
            //});
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
            lanView.ShowTabHeader = lanView.TabPages.Count > 1 ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;

            int el = 0;
            var key = plasas.FirstOrDefault()?.id;
            if (!string.IsNullOrEmpty(key))
            {
                Control[] cont = lanView.SelectedTabPage.Controls.Find(key, true);
                if (cont.Length > 0)
                {
                    var lv = ((Uixe.Watcher.Controls.LaneView)cont[0]);
                    el = lv.LaneCount;
                    sccMain.SplitterPosition = lanView.Height + 20;
                }
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
                var tb = from p in lanView.TabPages.ToArray() where p.Name == plaza select p;
                if (tb.Any())
                {
                    XtraTabPage xtp = tb.First();
                    Control[] cont = xtp.Controls.Find(plaza, true);
                    if (cont.Length > 0)
                    {
                        ((Uixe.Watcher.Controls.LaneView)cont[0]).ShowLaneInfor($"{plaza}{laneno}", revdata);
                    }
                }
            }
        }

        public void ShowLaneLost(string plaza, string laneno)
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
                        ((Uixe.Watcher.Controls.LaneView)cont[0]).ShowLaneLost(laneno);
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
        }



        [DebuggerStepThrough]
        private void tmNetworkTest_TickAsync(object sender, EventArgs e)
        {
            //if (sec != DateTime.Now.Second && client?.IsConnected == true)
            //{
            //    try
            //    {
            //        await client.PublishAsync("/tco/status/", new { message = "HeartBeat" });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"tmNetworkTest_TickAsync{ex.Message}");
            //    }
            //    sec = DateTime.Now.Second;
            //}
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