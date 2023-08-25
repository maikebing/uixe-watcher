using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher.Controls
{
    public partial class LaneView : DevExpress.XtraEditors.XtraUserControl
    {
        private ILogger _logger;

        /// <summary>
        /// InitLaneInfo 进行初始化时赋值。
        /// </summary>
        public Plaza Plaza { get; set; }

        private RuntimeSetting _runtimeSetting;
        public List<LaneInfo> lst = new List<LaneInfo>();

        public LaneView()
        {
            InitializeComponent();
        }

        internal void InitLaneInfo(Plaza item, ILogger logger, RuntimeSetting runtimeSetting)
        {
            _logger = logger;
            Plaza = item;
            _runtimeSetting = runtimeSetting;
            var _ls = new List<LaneInfo>();
            item.lanes?.ForEach(l =>
            {
                _ls.Add(new LaneInfo(item.id, l.lane_id, l.lane_no, l.ip));
            });
            lst.AddRange(_ls.Where(l => !string.IsNullOrEmpty(l.LaneName) && l.LaneName.StartsWith("E")).OrderByDescending(e => e.LaneName).ToArray());
            lst.AddRange(_ls.Where(l => !string.IsNullOrEmpty(l.LaneName) && l.LaneName.StartsWith("X")).OrderBy(e => e.LaneName).ToArray());
            laneInfoBindingSource.DataSource = lst;
            gcExitLanes.RefreshDataSource();
        }

        private delegate void HShowLaneInfo(string laneid, LaneStatus revdata);

        /// <summary>
        ///
        /// </summary>
        /// <param name="plaza">这里的plaza 已经调用到对应的控件了， 无需区分， 但是需要传值过来</param>
        /// <param name="laneno"></param>
        /// <param name="revdata"></param>
        public void ShowLaneInfor(string laneid, LaneStatus revdata)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new HShowLaneInfo(ShowLaneInfor), laneid, revdata);
            }
            else
            {
                lock (lst)
                {
                    int i = lst.FindIndex(f => f.PlazaId + f.LaneName == laneid);
                    if (i >= 0)
                    {
                        (laneInfoBindingSource[i] as LaneInfo)?.Parse(revdata);
                        gvExitLanes.RefreshRow(gvExitLanes.GetRowHandle(i));
                    }
                }
            }
        }

        public int LaneCount => lst.Count;

        private void gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            var i = gv.GetDataSourceRowIndex(e.RowHandle);
            if (gv != null && i >= 0)
            {
                var dr = (laneInfoBindingSource[gv.GetDataSourceRowIndex(e.RowHandle)] as LaneInfo);
                var netstatus = dr.NetworkStatus;
                e.HighPriority = !netstatus;
                if (!dr.NetworkStatus)
                {
                    e.Appearance.BackColor = Color.FromArgb(100, 255, 225, 225);
                    e.Appearance.ForeColor = Color.FromArgb(100, 176, 0, 22);
                }
            }
        }

        public void ShowLaneLost(string laneid)
        {
            int i = lst.FindIndex(f => f.LaneName == laneid);
            if (i >= 0)
            {
                (laneInfoBindingSource[i] as LaneInfo).NetworkStatus = false;
                gvExitLanes.RefreshRow(gvExitLanes.GetRowHandle(i));
            }
        }

        private void ShowCardBox(CustomRowCellEditEventArgs e)
        {
            var ripb = e.RepositoryItem as RepositoryItemProgressBar;
            if (ripb != null)
            {
                var row = gvExitLanes.GetDataRow(e.RowHandle);
                ripb.Maximum = int.Parse(row["CardBoxMax"] as string);
                ripb.Minimum = int.Parse(row["CardBoxNow"] as string);
            }
        }

        private void btnVNC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var fv = gvExitLanes.GetFocusedRow() as LaneInfo;
                if (fv != null)
                {
                    try
                    {
                        frmRemoteViewer viewer = new frmRemoteViewer(this.Plaza, fv);
                        viewer.Show();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"打开{fv.LaneName}-{fv.IPAddress}VNC时遇到错误");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取当前车道信息时遇到问题");
            }
        }
        DateTime lasconfig = DateTime.MinValue;
        public void SendHeartBeat()
        {
            bool config = DateTime.Now.Subtract(lasconfig).TotalMinutes > 5;
            Plaza?.lanes?.ForEach(async lane =>
        {
            try
            {
                Application.DoEvents();
                if (await lane.Ping())
                {
                    Application.DoEvents();
                    await lane.SendMsg("tco/status/", new { message = "HeartBeat" });
                    if (config)
                    {
                        Application.DoEvents();
                        await lane.SendMsg("tco/config/", new { agentIp = Plaza?.agentIp });
                    }
                }
                else
                {
                    _logger.LogWarning($"网络不通， 无法发送{lane.lane_no} {lane.ip}心跳。");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"无法发送{lane.lane_no} {lane.ip}心跳");
            }
        });
            if (config) lasconfig = DateTime.Now;

        }
        private void gcExitLanes_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                radialMenu1.ShowPopup(PointToScreen(e.Location), true);
            }
        }

        private void btnRemotLane_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var fv = gvExitLanes.GetFocusedRow() as LaneInfo;
                if (fv != null)
                {
                    frmRemoteLane lane = new frmRemoteLane(Plaza, fv);
                    lane._runtimeSetting = _runtimeSetting;
                    lane.Show(this);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "远程控制时获取当前车道信息时遇到问题");
            }
        }

        private void btnPing_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}