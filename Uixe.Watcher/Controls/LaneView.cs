using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;

namespace Uixe.Watcher.Controls
{
    public partial class LaneView : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// InitLaneInfo 进行初始化时赋值。
        /// </summary>
        public Plaza Plaza { get; set; }

        public List<LaneInfo> lst = new List<LaneInfo>();

        public LaneView()
        {
            InitializeComponent();
        }

        internal void InitLaneInfo(Plaza item)
        {
            Plaza = item;
            var _ls = new List<LaneInfo>();
            item.lanes?.ForEach(l =>
            {
                _ls.Add(new LaneInfo(item.id, l.lane_id, l.lane_no, l.ip) { CameraRtspUrl = l.cameraRtspUrl });
            });
            lst.AddRange(_ls.Where(l => !string.IsNullOrEmpty(l.LaneName) && l.LaneName.StartsWith("E")).OrderByDescending(e => e.LaneName).ToArray());
            lst.AddRange(_ls.Where(l => !string.IsNullOrEmpty(l.LaneName) && l.LaneName.StartsWith("X")).OrderBy(e => e.LaneName).ToArray());
            laneInfoBindingSource.DataSource = lst;
            gcExitLanes.RefreshDataSource();
        }

        private delegate void HShowLaneInfo(string laneid, string revdata);

        /// <summary>
        ///
        /// </summary>
        /// <param name="plaza">这里的plaza 已经调用到对应的控件了， 无需区分， 但是需要传值过来</param>
        /// <param name="laneno"></param>
        /// <param name="revdata"></param>
        public void ShowLaneInfor(string laneid, string revdata)
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
                    catch (Exception)
                    {
                    }
                }
            }
            catch
            {
            }
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
                    lane.Show(this);
                }
            }
            catch
            {
            }
        }
    }
}