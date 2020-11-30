using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
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
            item.lanes?.ForEach(l =>
            {
                lst.Add(new LaneInfo(item.plaza_id, l.lane_id,l.lane_no,l.ip));
            });
            laneInfoBindingSource.DataSource = lst;
             gcExitLanes .RefreshDataSource();
        }

        private delegate void HShowLaneInfo(string plaza, string laneno, string revdata);

        /// <summary>
        ///
        /// </summary>
        /// <param name="plaza">这里的plaza 已经调用到对应的控件了， 无需区分， 但是需要传值过来</param>
        /// <param name="laneno"></param>
        /// <param name="revdata"></param>
        public void ShowLaneInfor(string plaza, string laneno, string revdata)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new HShowLaneInfo(ShowLaneInfor), plaza, laneno, revdata);
            }
            else
            {
                lock (lst)
                {

                    var li = Newtonsoft.Json.JsonConvert.DeserializeObject<LaneInfo>(revdata);
                    var index = lst.FindIndex(l => l.LaneNo == li.LaneNo);
                    lst[index] = li;
                }
            }
        }
         
        public int LaneCount { get; set; }

        
        private void timer1_Tick(object sender, EventArgs e)
       {
            
       
        }

  

        private void gv_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            var dr = gv.GetDataRow(e.RowHandle);
            if (gv != null && dr != null)
            {
                bool badlane = gv.GetDataRow(e.RowHandle).Field<string>("badlane") == "1";
                e.HighPriority = badlane;
                if (badlane)
                {
                    e.Appearance.BackColor = Color.FromArgb(100, 255, 225, 225);
                    e.Appearance.ForeColor = Color.FromArgb(100, 176, 0, 22);
                }
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

   
     
    }
}