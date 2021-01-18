using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TCS.BOSS.VNC;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Properties;

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
            var _ls= new List<LaneInfo>();
            item.lanes?.ForEach(l =>
            {
                _ls.Add(new LaneInfo(item.id, l.lane_id,l.lane_no,l.ip));
            });
            lst.AddRange(_ls.Where(l => l.LaneName.StartsWith("E")).OrderByDescending(e => e.LaneName).ToArray());
            lst.AddRange(_ls.Where(l => l.LaneName.StartsWith("X")).OrderBy(e => e.LaneName).ToArray());
            laneInfoBindingSource.DataSource = lst;
             gcExitLanes .RefreshDataSource();
        }

        private delegate void HShowLaneInfo(string laneid, string revdata);

        /// <summary>
        ///
        /// </summary>
        /// <param name="plaza">这里的plaza 已经调用到对应的控件了， 无需区分， 但是需要传值过来</param>
        /// <param name="laneno"></param>
        /// <param name="revdata"></param>
        public void ShowLaneInfor(string laneid,string revdata)
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
                    if (i > 0)
                    {
                        (laneInfoBindingSource[i] as LaneInfo)?.Parse(revdata);
                        gvExitLanes.RefreshRow(gvExitLanes.GetRowHandle(i));
                    }
               
                }
            }
        }

        public int LaneCount => lst.Count;


        private void timer1_Tick(object sender, EventArgs e)
       {
            
       
        }

  

        private void gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            var dr = gv.GetDataRow(e.RowHandle);
            if (gv != null && dr != null)
            {
                //bool badlane = gv.GetDataRow(e.RowHandle).Field<string>("badlane") == "1";
                //e.HighPriority = badlane;
                //if (badlane)
                //{
                //    e.Appearance.BackColor = Color.FromArgb(100, 255, 225, 225);
                //    e.Appearance.ForeColor = Color.FromArgb(100, 176, 0, 22);
                //}
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

        private async void btnVNC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var fv = gvExitLanes.GetFocusedRow() as LaneInfo;
            if (fv != null)
            {
                var vnc=await VNCUtils.Login(this.ParentForm, fv.IPAddress, 5900, "kissme");
                vnc.Text = $"{fv.LaneName} {fv.IPAddress} ";
            }
        }

        private void gcExitLanes_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                radialMenu1.ShowPopup(PointToScreen( e.Location),true);
            }
        }
    }
}