using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.Templates;
using LiteDB;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.Devices;
using Renci.SshNet;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Uitls;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace Uixe.Watcher.Controls
{
    public partial class LaneView : DevExpress.XtraEditors.XtraUserControl
    {
        private frmPlaza _frmPlaza;
        private ILogger _logger;

        /// <summary>
        /// InitLaneInfo 进行初始化时赋值。
        /// </summary>
        public Plaza Plaza { get; set; }

        private RuntimeSetting _runtimeSetting;
        private AppSettings _settings;
        private LiteDB.ConnectionString _connection;
        public List<LaneInfo> lst = new List<LaneInfo>();

        public LaneView()
        {
            InitializeComponent();
        }

        internal void InitLaneInfo(Plaza item, ILogger logger, RuntimeSetting runtimeSetting, AppSettings settings, LiteDB.ConnectionString connection, frmPlaza frmPlaza)
        {
            _frmPlaza = frmPlaza;
            _logger = logger;
            Plaza = item;
            _runtimeSetting = runtimeSetting;
            _settings = settings;
            _connection = connection;
            var _ls = new List<LaneInfo>();
            item.lanes?.ForEach(l =>
            {
                _ls.Add(new LaneInfo(item.id, l.lane_id, l.lane_no, l.ip));
                Task.Run(async () =>
                {
                    var client = new RestClient(new RestClientOptions($"http://{l.ip}:10000/") { FollowRedirects = false });
                    client.AddDefaultHeader(KnownHeaders.Accept, "*/*");
                    var request = new RestRequest("/heartbeat", RestSharp.Method.Get);
                    var response = await client.GetAsync(request);
                });

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
                        //frmRemoteViewer viewer = new frmRemoteViewer(this.Plaza, fv);
                        //viewer.Show();
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
                    lane._settings = _settings;
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

        private void btnLaneReboot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var fv = gvExitLanes.GetFocusedRow() as LaneInfo;
                if (fv != null)
                {
                    try
                    {
                        using (var db = new LiteDatabase(_connection))
                        {

                            var tlane = db.GetCollection<t_lane>();
                            if (tlane != null)
                            {
                                var lane = tlane.FindOne(t => t.plazaid == fv.PlazaId && t.lane_no == fv.LaneName);
                                if (lane != null)
                                {
                                    if (string.IsNullOrEmpty(lane.password))
                                    {
                                        _frmPlaza.Alert("重启车道", "车道基础信息缺失,网络不通?");
                                    }
                                    else
                                    {
                                        using (SshClient ssh = new SshClient(lane.ip, lane.usename, lane.password))
                                        {
                                            var result = ssh.CreateCommand("reboot;exit;").Execute();
                                            _frmPlaza.Alert("重启车道", "重启命令执行完成:" + result);
                                        }
                                    }
                                }
                                else
                                {
                                    _frmPlaza.Alert("重启车道", $"车道{fv.PlazaId}{fv.LaneName}未能找到");
                                }
                            }
                            else
                            {
                                _frmPlaza.Alert("重启车道", $"车道本地基础信息异常");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "远程控制重启时遇到问题");
                        _frmPlaza.Alert("重启车道", $"远程控制重启时遇到问题1{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "远程控制时获取当前车道信息时遇到问题");
                _frmPlaza.Alert("重启车道", $"远程控制重启时遇到问题2{ex.Message}");
            }
        }
    }
}