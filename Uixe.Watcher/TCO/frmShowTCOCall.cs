using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.V1;
using Settings = Uixe.Watcher.Properties.Settings;
namespace Uixe.Watcher
{
    public partial class frmShowTCOCall : XtraForm
    {
        public frmShowTCOCall()
        {
            InitializeComponent();
            try
            {
                this.tsTabs.TabPages.Clear();
                var tmlLaneNo = Uitls.TollInfo.GetTollInfo();
                lstlane.AddRange(tmlLaneNo.lanes);
                for (int i = 0; i < lstlane.Count; i++)
                {
                    string pname = lstlane[i].lane_id+ tmlLaneNo.id;
                    XtraTabPage t = new XtraTabPage();
                    TCOConfirm tms = new TCOConfirm();
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
                Debug.WriteLine(ex.Message);
            }
        }

        private List<Lane> lstlane = new List<Lane>();

        public void Show(TCOCall TCOCallxxx)
        {
            string pname = TCOCallxxx.PlazaId + TCOCallxxx.LaneNo;
            var x = from p in tsTabs.TabPages where p.Name == pname select p;
            if (x.Any() && x.Single() != null)
            {
                XtraTabPage t = x.Single();
                TCOConfirm tms = (TCOConfirm)t.Tag;
                try
                {
               
                    tms.Show(TCOCallxxx);
                    t.Text = string.Format("车道{0}", TCOCallxxx.LaneNo);
                    tms.btnOK.Click += new EventHandler(BtnOK_Click);
                    tms.btnCancel.Click += new EventHandler(btnCancel_Click);
                    tsTabs.SelectedTabPage = t;
                    tms.Reset();
                    if ((TCOCallxxx.TCOTYPE == 5 && !Settings.Default.tco_chexingbufu)
                      || (TCOCallxxx.TCOTYPE == 6 && !Settings.Default.tco_chepaibufu)
                      || (TCOCallxxx.TCOTYPE == 7 && !Settings.Default.tco_chaoshichaosu))
                    {
                        tms.btnOK.PerformClick();
                    }
                }
                catch (Exception ex)
                {
                    tms.btnOK.Click -= new EventHandler(BtnOK_Click);
                    tms.btnCancel.Click -= new EventHandler(btnCancel_Click);
                }
                try
                {
                    t.PageVisible = true;
                    this.Visible = true;
                    this.Focus();
                }
                catch (Exception ex)
                {
                 Debug.WriteLine(string.Format("ERRORTCOCALL_SHOW:<{0}>\r\n{1}\r\n", ex.Message, TCOCallxxx));
                }
            }
            else
            {
                Debug.WriteLine("内部错误" + pname);
            }
        }

   

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(SimpleButton) && tsTabs.SelectedTabPage != null && ((SimpleButton)sender).Parent != null && ((SimpleButton)sender).Parent.Name == tsTabs.SelectedTabPage.Name)
            {
                if (this.tsTabs.SelectedTabPage.Tag is TCOConfirm tms && tms.CanDo)
                {
                    tms.btnOK.Click -= new EventHandler(BtnOK_Click);
                    tms.btnCancel.Click -= new EventHandler(btnCancel_Click);
                    Submit(false);
                    RemoveNowTab();
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (tsTabs.SelectedTabPage == null) return;
            if (sender != null && sender.GetType() == typeof(SimpleButton) && ((SimpleButton)sender).Parent != null && ((SimpleButton)sender).Parent.Name == tsTabs.SelectedTabPage.Name)
            {
                TCOConfirm tms = this.tsTabs.SelectedTabPage.Tag as TCOConfirm;
                if (!tms.CheckPlazaInfo())
                {
                    tms.ShowPlazaPopup();
                }
                else
                {
                    tms.btnOK.Click -= new EventHandler(BtnOK_Click);
                    tms.btnCancel.Click -= new EventHandler(btnCancel_Click);
                    Submit(true);
                    RemoveNowTab();
                }
            }
        }

        private void Submit(bool ok)
        {
            if (this.tsTabs.SelectedTabPage.Tag is TCOConfirm tms)
            {
                string cmd = "TCOCONFIRM";
                string param = tms.GetAUS(ok).ToString(); ;
               // BLLWatcher.SendReturnInfoToLane(TCS.DAL.DALWatcher.GetLaneInfo(tms.TCO.Head.Network + tms.TCO.Head.Plaza, tms.TCO.Head.LaneNo), cmd, param);
            }
        }

        private void RemoveNowTab()
        {
            if (tsTabs.SelectedTabPage != null)
            {
                tsTabs.SelectedTabPage.PageVisible = false;
            }
            var s = from p in tsTabs.TabPages where p.PageVisible == true select p;
            if (!s.Any())
            {
                this.Visible = false;
            }
        }

        private void frmShowTCOCall_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }
    }
}