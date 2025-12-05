using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;

namespace Uixe.Watcher.TCO
{

    public partial class frmWeightTCOCall : DevExpress.XtraEditors.XtraForm
    {
        public static int TabCount = 0;

        private readonly T_Plaza _plaza;
        private readonly RuntimeSetting _runtimeSetting;
        private readonly AppSettings _settings;
        private readonly ILogger _logger;

        public frmWeightTCOCall(T_Plaza plaza, RuntimeSetting runtimeSetting, AppSettings settings,  ILogger logger)
        {
            InitializeComponent();
            _plaza = plaza;
            _runtimeSetting = runtimeSetting;
            _settings=settings;
            _logger = logger;
        }
 
        public  void LoadInfo()
        {
            try
            {
                this.tsTabs.TabPages.Clear();
                for (int i = 0; i < _plaza.Lanes.Count; i++)
                {
                    string pname = _plaza.Id + _plaza.Lanes[i].LaneNo;
                    XtraTabPage t = new XtraTabPage();
                    WeightTCOConfirm tms = new WeightTCOConfirm();
                    tms._logger = _logger;  
                    tms._runtimeSetting = _runtimeSetting;
                    tms._settings = _settings;
                    tms.Plaza = _plaza;
                    tms.Lane = _plaza.Lanes[i];
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
                _logger.LogError(ex, "初始化TCO监控确认界面时遇到异常");
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
                    t.Text = string.Format($"车道:{pname}");
                    tms.Show(mu);
                    tms.Owner = this;
                    t.PageVisible = true;
                    this.TopMost = true;
                    this.Show();
                    t.Focus();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "展示监控确认信息时遇到问题异常");
                XtraMessageBox.Show($"展示监控确认信息时遇到问题异常:{ex.Message}");
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