using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher;
using MQTTnet.Client;

namespace Uixe.Watcher.V1
{
    public partial class frmWeightTCOCall : DevExpress.XtraEditors.XtraForm
    {
        public static int TabCount = 0;

    

        private List<Lane> lstlane = new List<Lane>();

        public frmMain Main { get; internal set; }

        public frmWeightTCOCall()
        {
            InitializeComponent();

        }
        public void LoadInfo(IMqttClient _mqttClient)
        {
            try
            {
                this.tsTabs.TabPages.Clear();
                var tmlLaneNo = Uitls.TollInfo.GetTollInfo();
                lstlane.AddRange(tmlLaneNo.lanes);
                for (int i = 0; i < lstlane.Count; i++)
                {
                    string pname = tmlLaneNo.id + lstlane[i].lane_no;
                    XtraTabPage t = new XtraTabPage();
                    WeightTCOConfirm tms = new WeightTCOConfirm();
                    tms.Main = this.Main;
                    tms.MqttClient = _mqttClient;
                    t.Name = pname;
                    tms.Name = pname;
                    tms.Parent = t;//由于在现实数据时使用到TabPage 在给TCO属性赋值前必须赋值Parent
                    tms.Dock = DockStyle.Fill;
                    t.Controls.Add(tms);
                    t.Tag = tms;
                    tsTabs.TabPages.Add(t);
                    t.PageVisible = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERRORINIT:<{0}>\r\n", ex.Message);
            }
        }

        public void ShowTCOMsg(MsgWeightTCOCALL mu)
        {
            try
            {
                string pname = GetPageName(mu);
                var x = from p in tsTabs.TabPages where p.Name == pname select p;
                if (x.Any())
                {
                    XtraTabPage t = x.Single(); ;
                    WeightTCOConfirm tms = t.Tag as WeightTCOConfirm;
                    t.Text = string.Format("站:{1}车道{0}", mu.LaneNo, mu.Plaza);
                    tms.Show(mu);
                    t.PageVisible = true;
                    this.Visible = true;
                    t.Focus();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("内部错误" + ex.Message);
            }
        }

        private static string GetPageName(MsgWeightTCOCALL mu)
        {
            return $"650{mu.Network}{ mu.Plaza}{ mu.LaneNo}";
        }

        public void RemoveNowTab(string name)
        {
            if (!this.IsDisposed && this.IsHandleCreated
                && !tsTabs.IsDisposed && tsTabs.IsHandleCreated
               )
            {
                try
                {
                    var s = from p in tsTabs.TabPages where p.Name == name select p;
                    if (s.Any())
                    {
                        XtraTabPage xtp = s.SingleOrDefault();
                        if (xtp != null && xtp.Tag != null)
                        {
                            if (xtp.Tag is WeightTCOConfirm tms)
                            {
                                xtp.PageVisible = false;
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            var s1 = from p in tsTabs.TabPages where p.PageVisible == true select p;
            if (!s1.Any())
            {
                this.Visible = false;
            }
        }
    }
}