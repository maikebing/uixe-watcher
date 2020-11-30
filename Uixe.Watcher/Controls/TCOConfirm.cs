using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Uixe.Watcher.Msg;
using Uixe.Watcher.V1;

namespace Uixe.Watcher
{
    public partial class TCOConfirm : DevExpress.XtraEditors.XtraUserControl
    {
        public TCOConfirm()
        {
            InitializeComponent();
        }


        private TCOCall TCE;
        public bool CanDo { get; set; }

        public bool CheckPlazaInfo()
        {
            bool ret = true;
            if (TCE.TCOTYPE == WATCHER_UnKnowPlaza)
            {
                string txt = cbxModifyEntryPlaza.GetColumnValue("NetNo") as string + cbxModifyEntryPlaza.GetColumnValue("PlazaNo") as string;
                if (string.IsNullOrEmpty(txt))
                {
                    ret = false;
                }
                else
                {
                    ret = true;
                }
            }
            return ret;
        }

        public void Show(TCOCall tce)
        {
            InitInfo();
            if (tce.TCOTYPE == WATCHER_PlateInBlack)
            {
                tcoPictureBox2.Visible = true;
            }
            else
            {
                tcoPictureBox2.Visible = false;
            }
            //   FillPlazaNameAndList(tce);
            //   LoadEntryPicture(tce);
            pLazaBindingSource.ResetCurrentItem();
            tCOCallBindingSource.ResetBindings(false);
            tCOCallBindingSource.ResetCurrentItem();
            AUS = new TCOAUS();
            TCE = tce;
            tCOCallBindingSource.DataSource = tce;
            chkCarKind.Checked = tce.DifClass != 0;
            chkCarPlate.Checked = tce.DifPlate;
            chkCarType.Checked = tce.DifType;

            chkEntryPlaza.Text = tce.DifPlaza ? "入口站不明" : "入口正常卡";
            chkEntryPlaza.Checked = tce.DifPlaza;
            chkIsU.Checked = tce.UCar;
            chkTimeoutCar.Checked = tce.TimeoutCar != 0;
            switch (tce.DifClass)
            {
                case 2:
                    chkCarKind.Visible = true;
                    chkCarKind.Text = "无卡公务";
                    chkCarKind.Checked = true;
                    break;

                case 3:
                    chkCarKind.Visible = true;
                    chkCarKind.Text = "绿色通道";
                    chkCarKind.Checked = true;
                    break;

                case 4:
                    chkCarKind.Visible = true;
                    chkCarKind.Text = "农用车";
                    chkCarKind.Checked = true;
                    break;

                default:
                    chkCarKind.Visible = false;
                    break;
            }
            switch (tce.TimeoutCar)
            {
                case 1:
                    chkTimeoutCar.Text = "超时车";
                    break;

                case 2:
                    chkTimeoutCar.Text = "超速车";
                    break;
            }
            txtcarnumber.Text = tce.ExitPlate;
            txtocartype.Text = tce.ExitCarType;

            AUS.DifPlaza = "0";
            AUS.DifPlate = "0";
            AUS.DifClass = "0";
            AUS.DifType = "0";
            switch (tce.TCOTYPE)
            {
                case 1://WATCHER_MenuCardBox
                    gcCardInfo.Visible = false;
                    break;

                default:
                    break;
            }
            if (string.IsNullOrEmpty(cbxModifyEntryPlaza.EditValue as string))
            {
                if (string.IsNullOrEmpty(txtentrysite.Text))
                {
                    cbxModifyEntryPlaza.EditValue = txtentrysite.Text;

                }
                else if (string.IsNullOrEmpty(txtexitsite.Text))
                {
                    cbxModifyEntryPlaza.EditValue = txtexitsite.Text;
                }
            }
        }

        private void InitInfo()
        {
            try
            {
                tcoPictureBox1.Image = null;
                chkTimeoutCar.Checked = false;
                gcCardInfo.Visible = true;
                chkIsU.Checked = false;
                chkCarType.Checked = false;
                chkCarPlate.Checked = false;
                chkEntryPlaza.Checked = false;
                chkCarKind.Checked = false;

                tCOCallBindingSource.DataSource = null;
                tCOCallBindingSource.Clear();
                pLazaBindingSource.Clear();
                pLazaBindingSource.Position = -1;
                pLazaBindingSource.ResetBindings(false);
                pLazaBindingSource.ResetCurrentItem();

                tCOCallBindingSource.ResetBindings(false);
                tCOCallBindingSource.ResetCurrentItem();
                gcCardInfo.Visible = true;
            }
            catch (Exception)
            {
            }
        }

        public void ShowPlazaPopup()
        {
            cbxModifyEntryPlaza.ShowPopup();
        }

        public void Reset()
        {
            this.pcPronow.Properties.Maximum = TCE.TimeOutLong;
            pcPronow.Position = TCE.TimeOutLong;
            timer1.Start();
            CanDo = true;
            btnOK.Enabled = true;
        }

        private void LoadEntryPicture(TCOCall tce)
        {
            new System.Threading.Thread((System.Threading.ThreadStart)delegate
            {
                try
                {
                    if (tce.TCOTYPE == WATCHER_PlateInBlack)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            //string url = string.Format("ftp://root:{0}@/{1}/{2}/IMAGE/TEMP/"
                            //     , AppConfig.RunTime.VNCPassword
                            //      , AppConfig.GetLaneIP(AppConfig.RunTime.NetNo + AppConfig.RunTime.PlazaNo, TCO.Head.LaneNo)
                            //       , AppConfig.RunTime.LaneAppDir
                            //    );
                            //tcoPictureBox1.ImageLocation = url + "TTEMP.JPG";
                            //tcoPictureBox2.ImageLocation = url + "3TEMP.JPG";
                        });
                    }
                    else
                    {
                        //TryFindImage(tce.EntryNetNo, tce.EntryPlazaNo, tce.EntryID);
                        //if (tcoPictureBox1.Image == null)
                        //{
                        //    var plza = TCS.Config.AppConfig.GetMyRoot();
                        //    TryFindImage(plza.NetNo, plza.PlazaNo, tce.EntryID);
                        //}
                    }
                }
                catch (System.Exception)
                {
                }
            }).Start();
        }



        private void FillPlazaNameAndList(TCOCall tce)
        {
            try
            {
                //pLazaBindingSource.DataSource = Config.AppConfig.Toll.Plaza;
                //var r = from p in Config.AppConfig.Toll.Plaza where p.NetNo == tce.EntryNetNo && p.PlazaNo == tce.EntryPlazaNo select p;
                //if (r.Any())
                //{
                //    string psn = r.FirstOrDefault().Plaza_Name;
                //    cbxModifyEntryPlaza.EditValue = psn;
                //    txtentrysite.Text = psn;
                //    txtexitsite.Text = psn;
                //}
                //else
                //{
                //    cbxModifyEntryPlaza.EditValue = "";
                //}
            }
            catch (System.Exception)
            {
            }
        }

        private void checkEdits_CheckedChanged(object sender, EventArgs e)
        {
            //CheckEdit ce = sender as CheckEdit;
            //ce.Properties.ValueChecked = ce.Checked;
            //ce.Properties.ValueUnchecked = !ce.Checked;
        }

        private  TCOAUS AUS;
        public const int WATCHER_UnKnowPlaza = 9;	//不明入口站
        public const int WATCHER_PlateInBlack = 20; //黑名单车辆

        public  TCOAUS GetAUS(bool IsSubmit)
        {
             TCOCall tc = TCE;
            AUS.CarAxleNumber = tc.CarAxleNumber;
            AUS.CarClass = AUS.DifClass == "1" ? txtModifyCarKind.Text : tc.ExitCarClass;
            AUS.CarPlate = AUS.DifPlate == "1" ? txtModifyCarNumber.Text : tc.ExitPlate;
            AUS.CarType = AUS.DifType == "1" ? txtModifyCarType.SelectedIndex.ToString("00") : tc.ExitCarType;
            string txt = cbxModifyEntryPlaza.GetColumnValue("NetNo") as string + cbxModifyEntryPlaza.GetColumnValue("PlazaNo") as string;
            //if (!string.IsNullOrWhiteSpace(txt) && TCS.Config.AppConfig.GetPlaza(txt) != null)
            //{
            //    AUS.NetNo = AUS.DifPlaza == "1" ? txt.Substring(0, 2) : tc.EntryNetNo;
            //    AUS.PlazaNo = AUS.DifPlaza == "1" ? txt.Substring(2, 2) : tc.EntryPlazaNo;
            //}
            AUS.CarWeight = tc.CarWeight;
          //  AUS.RouteNo = "";
            AUS.TransNo = tc.TransNo;
            AUS.TimeoutCar = tc.TimeoutCar.ToString();
            AUS.TCOStaffID = RuntimeSetting.NowCollect != null ? RuntimeSetting.NowCollect.UserId : "";
            AUS.UCar = tc.UCar.ToString ();
            AUS.ConfirmType = IsSubmit;
            return AUS;
        }

        private void cbxModifyEntryPlaza_TextChanged(object sender, EventArgs e)
        {
            if (AUS != null) AUS.DifPlaza = "1";
        }

        private void txtModifyCarKind_TextChanged(object sender, EventArgs e)
        {
            if (AUS != null) AUS.DifClass = "1";
        }

        private void txtModifyCarNumber_TextChanged(object sender, EventArgs e)
        {
            if (AUS != null) AUS.DifPlate = "1";
        }

        private void btnReadPicture_Click(object sender, EventArgs e)
        {

        }

        private DateTime dtold = DateTime.Now;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dtold.Second != DateTime.Now.Second)
            {
                dtold = DateTime.Now;
                pcPronow.Position = pcPronow.Position - 1;
                if ( TCOCallUtils._tcocall != null && TCOCallUtils._tcocall.Visible)
                {
                    //  BLLWatcher.TellLaneTCOIsRE(TCO.Head.Network + TCO.Head.Plaza, TCO.Head.LaneNo);
                }
                if (pcPronow.Position == pcPronow.Properties.Minimum)
                {
                    timer1.Stop();
                    btnOK.Enabled = false;
                    CanDo = false;
                }
            }
        }

        private void txtModifyCarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AUS != null && txtModifyCarType.SelectedIndex > 0) AUS.DifType = "1";
        }

        public Vnc.Viewer.View vnc;

        private void btnVNC_Click(object sender, EventArgs e)
        {
            //string ip = TCS.Config.AppConfig.GetLaneIP(TCO.Head.Network + TCO.Head.Plaza, TCO.Head.LaneNo);
            //try
            //{
            //    if (vnc == null || vnc.IsDisposed || !vnc.IsHandleCreated)
            //    {
            //        vnc = await VNCUtils.Login(Uixe.Watcher.Program.MainForm,ip, 5900, AppConfig.RunTime.VNCPassword);
            //    }
            //    vnc.Show();
            //    vnc.Focus();
            //}
            //catch (Exception ex)
            //{
            //    Log.LogException("btnVNC_Click", ip, ex);
            //}
        }

        private void pcPronow_DoubleClick(object sender, EventArgs e)
        {
        }

        private void btnQueryOnline_Click(object sender, EventArgs e)
        {
        }
    }
}